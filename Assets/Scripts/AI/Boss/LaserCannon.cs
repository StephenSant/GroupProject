using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannon : MonoBehaviour
{

    public Transform muzzle;
    public float damage = 10f;
    public LineRenderer laser;
    public float delayBetweenShots;
    public float weaponRange = 75f;
    public float distanceToTarget;
    public WaitForSeconds shotDuration = new WaitForSeconds(0.2f);
    public float rateOfFire = 0.12f;
    //public AudioSource soundSource;
    //public AudioClip[] soundclips;
    public LayerMask targetMask;


    // Use this for initialization
    void Start()
    {
        laser = GetComponentInChildren<LineRenderer>();
        //soundSource = GetComponentInChildren<AudioSource>(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = GetComponentInParent<BossAI>().targetPos;
        transform.LookAt(targetPosition);
        Vector3 origin = muzzle.transform.position;
        RaycastHit hit;
        Physics.Raycast(muzzle.position, transform.TransformDirection(Vector3.forward), out hit, weaponRange, targetMask);

            if (hit.collider)
            {
                Vector3 direction = (hit.point - muzzle.position).normalized;

                laser.SetPosition(0, muzzle.position);
                DealDamage();

            }

    }
    void DealDamage()
    {

        StartCoroutine(Shooting());
        Vector3 origin = muzzle.transform.position;
        
        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, transform.TransformDirection(Vector3.forward), out hit, weaponRange, targetMask))
        {
            if (hit.collider.tag == "Player")
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
                //laser.SetPosition(1, origin + (transform.forward * weaponRange));
            }
        }
    }
    private IEnumerator Shooting()
    {
        laser.enabled = true;
        //soundSource.clip = soundclips[0];
        //soundSource.Play();
        yield return shotDuration;
        laser.enabled = false;
    }

}
