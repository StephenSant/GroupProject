using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperWeapon : MonoBehaviour
{
    public float damage = 80f;
    public int currentAmmo;
    public float reloadTime = 4.2f;
    public float delayBetweenShots = 0.2f;
    public float fireRate = 1f;
    public int magCap = 10;
    public Transform muzzle;
    public float range = 100;
    public Camera playerCam;
    public float nextFire;

    private bool canFire;
    private LineRenderer bulletTrail;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.3f);
	// Use this for initialization
	void Start ()
    {
        bulletTrail = GetComponent<LineRenderer>();
        playerCam = GetComponentInParent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
        {
            Shoot();

        }
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
    }
    void Shoot()
    {
        nextFire = Time.time + fireRate;

        StartCoroutine(ShotEffect());
    }
    void Reload()
    {

    }
    private IEnumerator ShotEffect()
    {
        bulletTrail.enabled = true;

        yield return shotDuration;

        bulletTrail.enabled = false;
    }
}
