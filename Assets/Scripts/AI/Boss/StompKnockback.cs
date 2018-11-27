using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompKnockback : MonoBehaviour
{
    public float knockRadius = 360f; //the knockback Radius
    public float knockPower = 10f;//Knockback power
    public Vector3 newForce;


    // Use this for initialization


    // Update is called once per frame
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Rigidbody pRigid = other.gameObject.GetComponent<Rigidbody>();
            if (pRigid != null)
            {
                //pRigid.AddExplosionForce(1000f, transform.position, 360f, 20f, ForceMode.VelocityChange);
                //other.transform.Translate(0, 1, -10);

            }

        }
    }
}
