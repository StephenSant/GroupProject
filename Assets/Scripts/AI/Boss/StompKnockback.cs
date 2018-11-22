using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompKnockback : MonoBehaviour
{
    public float knockRadius = 3.5f; //the knockback Radius
    public float knockPower = 1f;//Knockback power
    public Vector3 newForce;

    // Use this for initialization


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 knockBackPos = transform.position;//position where the knockback happens
        Collider[] colliders = Physics.OverlapSphere(knockBackPos, knockRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (hit.tag == "Player")
            {

                newForce = rb.transform.position - transform.position;
                rb.AddForce((rb.transform.position - transform.position).normalized * 500f);
            }
        }

    }
}
