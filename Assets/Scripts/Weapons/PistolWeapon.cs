using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PistolWeapon : MonoBehaviour
{
    #region Weapon Stats
    [Header("Weapon Stats")]
    public float damage = 25f;
    public float reloadTime = 3.1f;
    public float delayBetweenShots = 0.1f;
    public float fireRate = 1f;
    public int magCap = 12;
    public int currentAmmo, firedShots, remainingAmmo;
    public float weaponRange = 20f;
    public float laserRange = 100f;
    #endregion
    #region Weapon Components
    [Header("Weapon Components")]
    public Transform muzzle;
    #endregion
    #region Player Cam
    public Camera playerCam;
    #endregion
    #region Weapon Functions
    public float nextFire;
    public Text ammoText;
    private bool reloading;
    private float rayDistance = 25f;
    private bool canFire;
    public Transform laserSight;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.3f);

    #endregion
    #region Particles
    public GameObject bulletParticle;
    #endregion
    public Texture2D crosshair;
    // Use this for initialization
    void Start()
    {
        currentAmmo = magCap;

        playerCam = Camera.main;
    }
    private void OnDrawGizmos()
    {
        Ray aimray = new Ray(transform.position, Vector3.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(aimray.origin, aimray.origin + aimray.direction * rayDistance);
    }

    public void LateUpdate()

    {
        // Detect collision with wall (Raycast to wall)
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        Physics.Raycast(rayOrigin, playerCam.transform.forward, out hit, weaponRange + laserRange);
        // If Raycast hits wall
        if (hit.collider)
        {// Rotate gun to hit point - Quaternion.LookRotation(direction)
            Vector3 relativePos = hit.point - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            laserSight.position = hit.point;
        }
        else
        {
            Vector3 relativePos = playerCam.transform.forward * laserRange;
            transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            laserSight.position = playerCam.transform.forward * laserRange;
        }

    }
    private void Update()
    {
        // If mouse button down
        // Shoot bullet

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire && currentAmmo > 0 && Time.timeScale == 1 && !reloading)
        {
            Shoot();
            currentAmmo -= 1;
            firedShots += 1;

        }
        if (reloading)
        {
            ammoText.color = Color.red;
        }
        else if (reloading == false)
        {
            ammoText.color = Color.black;
            StopCoroutine(ReloadingSequence());
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magCap)
        {
            StartCoroutine(ReloadingSequence());
        }
        if (Input.GetKey(KeyCode.Mouse0) && currentAmmo <= 0 && remainingAmmo > 0 && Time.timeScale == 1)
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
        SpawnCollider();
        StartCoroutine(ShotEffect());
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        //bulletTrail.SetPosition(0, muzzle.position);
        if (Physics.Raycast(rayOrigin, playerCam.transform.forward, out hit, weaponRange))
        {
            Instantiate(bulletParticle, hit.point, Quaternion.identity);
            Vector3 direction = (hit.point - muzzle.position).normalized;
            //bulletTrail.SetPosition(1, hit.point);

            MinionHealth enemyHealth = hit.collider.GetComponent<MinionHealth>();
            Health hHandler = hit.collider.GetComponent<Health>();
            WeakpointHealth weakpoint = hit.collider.GetComponent<WeakpointHealth>();
            if (hHandler != null)
            {
                hHandler.TakeDamage(damage);
            }
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            if (weakpoint != null)
            {
                weakpoint.TakeDamage(damage);
            }
        }
        else
        {
            //bulletTrail.SetPosition(1, playerCam.transform.forward * weaponRange);
        }
    }

    private IEnumerator ShotEffect()
    {
        if (reloading == false)
        {
            //soundsource plays
        }

        yield return shotDuration;
    }
    private IEnumerator ReloadingSequence()
    {
        if (!reloading)
        {
            //soundsource plays
        }
        reloading = true;
        yield return new WaitForSeconds(3.5f);

        if (reloading)
        {
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
        }
        firedShots = 0;
        reloading = false;
    }
    public void AmmoLoadedText()
    {
        ammoText.text = "" + currentAmmo.ToString();
    }

    public void SpawnCollider()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Enemy")
            {
                BehaviourAI enemy = hitColliders[i].GetComponent<BehaviourAI>();
                enemy.Investigate(transform.position);
            }
            i++;
        }
    }
    private void OnGUI()
    {
        float xMin = (Screen.width / 2) - (crosshair.width / 2);
        float yMin = (Screen.height / 2) - (crosshair.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width, crosshair.height), crosshair);
    }
}
