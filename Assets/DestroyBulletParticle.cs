using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBulletParticle : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //literally just destroys the empty gameobject with particle effects
        Destroy(gameObject, 1.5f);
	}
}
