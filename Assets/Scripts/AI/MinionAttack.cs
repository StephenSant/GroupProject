using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttack : MonoBehaviour
{
    public Transform muzzle;
    public float damage = 1f;
    public LineRenderer laser;
    public float delayBetweenShots;
    public float weaponRange = 75f;
    public float distanceToTarget;
    public WaitForSeconds shotDuration = new WaitForSeconds(1f);
    public float rateOfFire = 1f;
    public float readyToFire;
    // Use this for initialization
    void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = gameObject.transform.forward;
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, weaponRange);
        if (hit.collider)
        {
            Vector3 direction = (hit.point - muzzle.position).normalized;
            laser.SetPosition(0, muzzle.position);
            DealDamage();

        }
    }
    void DealDamage()
    {
        readyToFire = Time.time + rateOfFire;
        StartCoroutine(Shooting());
        Vector3 origin = gameObject.transform.forward;
        laser.SetPosition(0, muzzle.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, weaponRange))
        {
            if (hit.collider)
            {
                laser.SetPosition(1, hit.point);
                Vector3 direction = (hit.point - muzzle.position).normalized;
                PlayerHealth pHealth = hit.collider.GetComponent<PlayerHealth>();
                if (pHealth != null)
                {
                    pHealth.TakeDamage(damage);
                }


            }
            else
            {
                laser.SetPosition(1, origin + (transform.forward * weaponRange));
            }
        }
    }
    public IEnumerator Shooting()
    {
        laser.enabled = true;
        yield return shotDuration;
        laser.enabled = false;
    }
}
