using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttack : MonoBehaviour
{
    public Transform muzzle;
    public float damage = 10f;
    public LineRenderer laser;
    public float delayBetweenShots;
    public float weaponRange = 75f;
    public float distanceToTarget;
    public WaitForSeconds shotDuration = new WaitForSeconds(0.2f);
    public float rateOfFire = 0.12f;
    public float readyToFire;
    public AudioSource soundSource;
    public AudioClip[] soundclips;
    public LayerMask targetMask;

    // Use this for initialization
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        soundSource = GetComponentInChildren<AudioSource>(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = gameObject.transform.forward;
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, weaponRange, targetMask);
        if (Time.time > readyToFire)
        {
            if (hit.collider)
            {
                Vector3 direction = (hit.point - muzzle.position).normalized;
                laser.SetPosition(0, muzzle.position);
                DealDamage();

            }
        }

    }
    void DealDamage()
    {
        readyToFire = Time.time + rateOfFire;
        StartCoroutine(Shooting());
        Vector3 origin = gameObject.transform.forward;
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, weaponRange, targetMask))
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
    private IEnumerator Shooting()
    {
        laser.enabled = true;
        soundSource.clip = soundclips[0];
        soundSource.Play();
        yield return shotDuration;
        laser.enabled = false;
    }
}
