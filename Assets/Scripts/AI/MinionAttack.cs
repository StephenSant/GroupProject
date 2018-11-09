using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttack : MonoBehaviour
{
    public Transform muzzle;
    public float damage = 20f;
    public LineRenderer laser;
    public float delayBetweenShots;
    public float weaponRange = 50f;
    public float distanceToTarget;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, weaponRange));
	}
}
