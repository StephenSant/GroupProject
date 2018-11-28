using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompKnockback : MonoBehaviour
{
    public float knockRadius = 360f; //the knockback Radius
    public float knockPower = 10f;//Knockback power


    //public Vector3 newForce;
    //private void KnockBack()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
    //    foreach (Collider hit in colliders)
    //    {
    //        Rigidbody pRigid = hit.GetComponent<Rigidbody>();
    //        if (hit.tag == "Player")
    //        {

    //            Vector3 knockBackPos = hit.transform.position - transform.position;
    //            Vector3 launchLocation = new Vector3(knockBackPos.x, 120, knockBackPos.z);
    //            pRigid.AddForce(knockBackPos * 10, ForceMode.Impulse);
    //        }
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Player")
        {
            Rigidbody pRigid = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 knockBackPos = transform.position - collision.transform.position;
            Vector3 launchLocation = new Vector3(knockBackPos.x, 10, knockBackPos.z);
            pRigid.AddForce(launchLocation * 10, ForceMode.Impulse);
            //KnockBack();
        }
    }
    //void OnCollisionEnter(Collision other)
    //{

    //if (other.gameObject.tag == "Player")
    //{
    //    Rigidbody pRigid = other.gameObject.GetComponent<Rigidbody>();
    //    if (pRigid != null)
    //    {
    //        pRigid.AddExplosionForce(100f, transform.position , 1000f, .25f, ForceMode.VelocityChange);
    //        //other.transform.Translate(0, 1, -10);

    //    }

    //}
    //}
}
