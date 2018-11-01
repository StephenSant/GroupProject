using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperWeapon : MonoBehaviour
{
    public float damage = 80f;
    public int currentAmmo, firedShots, remainingAmmo;  
    public float reloadTime = 3.1f;
    public float delayBetweenShots = 0.2f;
    public float fireRate = 1f;
    public int magCap = 10;
    public Transform muzzle;
    public float weaponRange = 100;
    public Camera playerCam;
    public float nextFire;
    public Text left;

    public Transform hitPoint;

    public Transform laserSight;

    private bool reloading;
    private float rayDistance = 100f;
    private bool canFire;
    private LineRenderer bulletTrail;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.3f);
    // Use this for initialization
    void Start()
    {
        currentAmmo = magCap;
        bulletTrail = GetComponent<LineRenderer>();
        playerCam = Camera.main;
    }
    private void OnDrawGizmos()
    {
        Ray aimray = new Ray(transform.position, Vector3.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(aimray.origin, aimray.origin + aimray.direction * rayDistance);
    }

    private void LateUpdate()
    {
        // Detect collision with wall (Raycast to wall)
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        Physics.Raycast(rayOrigin, playerCam.transform.forward, out hit, weaponRange);
        // If Raycast hits wall
        if (hit.collider)
        {// Rotate gun to hit point - Quaternion.LookRotation(direction)
            Vector3 relativePos = hit.point - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            laserSight.position = hit.point;
        }
        else
        {
            Vector3 relativePos = playerCam.transform.forward * weaponRange - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            laserSight.position = relativePos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If mouse button down
        // Shoot bullet

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire && currentAmmo > 0)
        {
            Shoot();
            currentAmmo -= 1;
            firedShots += 1;

        }
        if (reloading == false)
        {
            StopCoroutine(ReloadingSequence());
        }
        if (Input.GetKeyDown(KeyCode.R) && remainingAmmo < magCap)
        {
            StartCoroutine(ReloadingSequence());
        }
        if (Input.GetKey(KeyCode.Mouse0) && currentAmmo <= 0 && remainingAmmo > 0)
        {
            StartCoroutine(ReloadingSequence());
        }
        if (remainingAmmo <= 0)
        {
            remainingAmmo = 0;
        }
        AmmoLoadedText();


    }
    void Shoot()
    {
        nextFire = Time.time + fireRate;

        StartCoroutine(ShotEffect());
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        bulletTrail.SetPosition(0, muzzle.position);
        if (Physics.Raycast(rayOrigin, playerCam.transform.forward, out hit, weaponRange))
        {
            Vector3 direction = (hit.point - muzzle.position).normalized;
            bulletTrail.SetPosition(1, hit.point);
        }
        else
        {
            bulletTrail.SetPosition(1, playerCam.transform.forward * weaponRange);
        }
    }

    private IEnumerator ShotEffect()
    {
        bulletTrail.enabled = true;

        yield return shotDuration;

        bulletTrail.enabled = false;
    }
    private IEnumerator ReloadingSequence()
    {
        reloading = true;
        yield return new WaitForSeconds(3.5f);
        if (currentAmmo > 0)
        {
            remainingAmmo -= firedShots;
            currentAmmo = magCap;
        }
        if (currentAmmo <= 0)
        {
            remainingAmmo -= magCap;
            currentAmmo = magCap;
        }
        if (currentAmmo > 0 && remainingAmmo >= 0)
        {
            currentAmmo += remainingAmmo;
            remainingAmmo -= firedShots;
        }
        firedShots = 0;
        reloading = false;
    }
    public void AmmoLoadedText()
    {
        left.text = "" + currentAmmo.ToString();
    }
}
