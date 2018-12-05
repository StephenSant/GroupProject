using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public Health hHandler;
    public bool exploded;
    public GameObject explosionEffect;

    // Use this for initialization
    void Start()
    {
        exploded = false;
        hHandler = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hHandler.curHealth <= 0)
        {
            if (!exploded)
            {
                Distract();
                Explode();
                Destroythis();
            }

            //exploded = true;
            //if (!exploded)
            //{
            //    //Explode();
            //    Distract();
            //    exploded = true;

            //}

        }
    }

    public void Explode()
    {
        //GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<MeshRenderer>().enabled = false;
        //GetComponent<MeshCollider>().enabled = false;

        explosionEffect = Instantiate(explosionEffect, transform.position, transform.rotation);
    }
    public void Distract()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, 100f);
        int i = 0;
        while (i < nearbyColliders.Length)
        {
            if (nearbyColliders[i].tag == "Enemy")
            {
                AI_ScoutDrone scoutDrone = nearbyColliders[i].GetComponent<AI_ScoutDrone>();
                scoutDrone.Investigate(transform.position);
            }
            i++;
        }
    }
    void Destroythis()
    {
        Destroy(gameObject);
    }
}
