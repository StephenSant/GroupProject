using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public ParticleSystem laser;
    public float range = 1000f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            if (hit.collider)
            {
                laser.transform.position = hit.point;
            }
            //laser.transform.position = hit.point;
        }
	}
}
