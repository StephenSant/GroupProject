using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBombardment : MonoBehaviour
{
    public Transform target;
    public float waitTime = 1.5f;
    public float range;
    public GameObject rocket;
	// Use this for initialization
	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Instantiate(rocket, transform.position, Quaternion.identity, target.transform);
	}
}
