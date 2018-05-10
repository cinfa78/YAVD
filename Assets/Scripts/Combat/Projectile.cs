using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class Projectile : MonoBehaviour, IProjectile {

    Collider coll;
    public SFloatValue damage;
    public SFloatValue speed;
    public SBoolValue isDeflectable;
    bool deflectable;
    public SAudio shootSound;
    public SAudio hitSound;
    public SAudio deflectSound;
    AudioSource audioSource;
    SEvent hitEvent;
    EventInfo projectileHitInfo;

    void Awake () {
        coll = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        projectileHitInfo = new EventInfo();
        projectileHitInfo.AddFloat("damage", damage.Value);
        deflectable = isDeflectable.Value;
    }

    public void OnSpawn()
    {
        deflectable = isDeflectable.Value;
        shootSound.Play(audioSource);
    }

    public void OnDespawn()
    {
        enabled = false;
    }

    public void OnHit() {
        hitSound.Play(audioSource);
        enabled = false;
    }

    public void OnDeflect()
    {
        if (deflectable) {
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
        if (tag == "Player")
        {
            hitEvent.Raise(projectileHitInfo);
        }
        if (collidedLayer == SortingLayer.NameToID("Map"))
        {
            OnHit();
        }
    }
}
