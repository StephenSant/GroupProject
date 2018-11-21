using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float damage = 150f;
    public Rigidbody rocketRigid;
    public float explosionForce = 1.5f;
    public ParticleSystem explosionEffect;

    // Use this for initialization
    void Start()
    {
        rocketRigid = GetComponent<Rigidbody>();
        rocketRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

    }

    // Update is called once per frame

    public void OnCollisionEnter(Collision collision)
    {

        //Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        //foreach (Collider nearbyObject in colliders)
        //    Object other = collision.collider.GetComponent<Rigidbody>();
        //if (other)
        {
            Explosion();
            SpawnExplosionParticle();
            
            //    PlayerHealth phealth = GetComponent<PlayerHealth>();
            //    phealth.TakeDamage(damage);
            //if (nearbyObject.tag == "Player")
            //{

            //    PlayerHealth phealth = GetComponent<PlayerHealth>();
            //    phealth.TakeDamage(damage);
            //}
            DestroyRocket();
        }
    }
    public void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            rocketRigid.AddExplosionForce(explosionForce, transform.position, explosionRadius,5f, ForceMode.Impulse);



            if (nearbyObject.tag == "Player")
            {
                Rigidbody rbody = nearbyObject.GetComponent<Rigidbody>();
                //rbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, 10f, ForceMode.Impulse);
                PlayerHealth pHealth = rbody.gameObject.GetComponent<PlayerHealth>();
                pHealth.TakeDamage(damage);
                //Destroy(gameObject);
            }
        }
    }

    public void SpawnExplosionParticle()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
    }
    public void DestroyRocket()
    {
        Destroy(gameObject);
    }
}