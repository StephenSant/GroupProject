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
                Explode();
                exploded = true;
                
            }
            
        }
    }
    void Explode()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;

        Instantiate(explosionEffect, transform.position, transform.rotation, transform);
    }
}
