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
    float internalSpeed;
    public SBoolValue isDeflectable;
    bool deflectable;
    public SAudio shootSound;
    public SAudio hitSound;
    public SAudio deflectSound;
    AudioSource audioSource;
    public SEvent hitEvent;
    //public EventInfo projectileHitInfo;
    public string fxPoolTag;
    public GameObject owner;

    void Awake()
    {
        //coll = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        deflectable = isDeflectable.Value;
        internalSpeed = speed.Value;
    }

    public void OnSpawn()
    {
        internalSpeed = speed.Value;
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
    public void Hit(float damageReceived, Vector3 hitter)
    {
        OnDeflect();
    }

    public void OnHit()
    {
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
            internalSpeed += 1f;
            deflectable = false;
            owner = gameObject;
        }
        else
        {
            OnHit();
        }

    }

    void Update()
    {
        transform.position += transform.forward * internalSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string collidedTag = collision.gameObject.tag;
        int collidedLayer = collision.gameObject.layer;

        if ((collidedTag == "Player" || collidedTag == "Monster") && collision.gameObject.GetInstanceID() != owner.GetInstanceID())
        {            
            if (collision.gameObject.GetComponent<IDamageable>() != null )
                collision.gameObject.GetComponent<IDamageable>().Hit(damage.Value);
            OnHit();
        }
        else if (collidedLayer == 12)
        {
            OnHit();
        }

    }
}
