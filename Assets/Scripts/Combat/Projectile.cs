using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class Projectile : MonoBehaviour, IProjectile, IDamageable
{
    //Collider coll;
    public SFloatValue damage;
    public SFloatValue speed;
    public SBoolValue isDeflectable;
    bool deflectable;
    public SAudio shootSound;
    public SAudio hitSound;
    public SAudio deflectSound;
    AudioSource audioSource;
    public SEvent hitEvent;
    //public EventInfo projectileHitInfo;
    public string fxPoolTag;

    void Awake()
    {
        //coll = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        deflectable = isDeflectable.Value;
    }

    public void OnSpawn()
    {
        deflectable = isDeflectable.Value;
        shootSound.Play(audioSource);
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    public void Hit(float damageReceived)
    {
        OnDeflect();
    }

    public void OnHit()
    {
        print(name + " projectile onhit");
        hitSound.Play(audioSource);
        if (fxPoolTag.Length > 0)
            GameObjectPool.instance.Spawn(fxPoolTag, transform.position, transform.rotation, Vector3.one);
        gameObject.SetActive(false);
    }

    public void OnDeflect()
    {
        if (deflectable)
        {
            deflectSound.Play(audioSource);
            transform.forward *= -1;
            deflectable = false;
        }
        else
        {
            OnHit();
        }

    }

    void Update()
    {
        transform.position += transform.forward * speed.Value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string collidedTag = collision.gameObject.tag;
        int collidedLayer = collision.gameObject.layer;
        //        print(name + " collided with " + collision.gameObject.name + " " + collidedTag);
        if (collidedTag == "Player")
        {
            if (collision.gameObject.GetComponent<IDamageable>() != null)
                collision.gameObject.GetComponent<IDamageable>().Hit(damage.Value);
            OnHit();
        }
        else if (collidedLayer == 12)
        {
            OnHit();
        }
    }
}
