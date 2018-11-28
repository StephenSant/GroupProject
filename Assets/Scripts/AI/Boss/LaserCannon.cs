using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannon : MonoBehaviour
{

    public Transform muzzle;
    public float damage = 10f;
    public LineRenderer laser;
    public float delayBetweenShots = 0.2f;
    public float shotDuration = 0.25f;
    public float weaponRange = 75f;
    //public AudioSource soundSource;
    //public AudioClip[] soundclips;
    //public LayerMask targetMask;


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
        transform.LookAt(new Vector3(targetPosition.x, Mathf.Clamp(targetPosition.y,transform.position.y - 10, transform.position.y + 10),targetPosition.z));
        Vector3 origin = muzzle.transform.position;
        RaycastHit hit;
        Physics.Raycast(muzzle.position, transform.TransformDirection(Vector3.forward), out hit, weaponRange);
        if (hit.collider)
        {
            if (hit.collider.CompareTag("Player") && laser.enabled)
            {
                laser.SetPosition(0, muzzle.position);
                laser.SetPosition(1, GameObject.Find("Player").transform.position);
                DealDamage();
            }
            else
            {
                laser.SetPosition(0, Vector3.zero);
                laser.SetPosition(1, Vector3.zero);
            }
        }
    }
    void DealDamage()
    {
        PlayerHealth pHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        StartCoroutine(Shooting());
        if (pHealth != null)
        {
            pHealth.TakeDamage(damage);
        }
        #region old script
        //Vector3 origin = muzzle.transform.position;

        //RaycastHit hit;
        //if (Physics.Raycast(muzzle.position, transform.TransformDirection(Vector3.forward), out hit, weaponRange, targetMask))
        //{
        //    if (hit.collider.tag == "Player")
        //    {
        //        laser.SetPosition(1, hit.point);
        //        Vector3 direction = (hit.point - muzzle.position).normalized;
        //        PlayerHealth pHealth = hit.collider.GetComponent<PlayerHealth>();
        //        if (pHealth != null)
        //        {
        //            pHealth.TakeDamage(damage);
        //        }


        //    }
        //    else
        //    {
        //        //laser.SetPosition(1, origin + (transform.forward * weaponRange));
        //    }
        //}
        #endregion
    }
    private IEnumerator Shooting()
    {
        laser.enabled = true;
        //soundSource.clip = soundclips[0];
        //soundSource.Play();
        yield return new WaitForSeconds(shotDuration);
        laser.enabled = false;
        yield return new WaitForSeconds(delayBetweenShots);
        laser.enabled = true;

    }

}
