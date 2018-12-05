using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperWeapon : MonoBehaviour
{
    #region Weapon Stats
    [Header("Weapon Stats")]
    public float damage = 100f;
    public int currentAmmo, firedShots, remainingAmmo;
    public float reloadTime = 3.1f;
    public float delayBetweenShots = 0.2f;
    public float fireRate = 1f;
    public int magCap = 10;
    public float weaponRange = 100f;
    #endregion
    #region Weapon Components
    [Header("Weapon Components")]
    public Transform muzzle;
    //public SphereCollider soundCollider;
//    public GameObject soundPoint;
    #endregion
    #region Player Cam
    public Camera playerCam;
    #endregion
    #region Weapon Functions
    public float nextFire;
    public Text ammoText;
    public Transform laserSight;
    public AudioSource soundSource;
    public AudioClip[] soundclips;
    private bool reloading;
    private float rayDistance = 100f;
    private bool canFire;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.3f);
    public float laserRange = 100f;
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
        soundSource = GameObject.Find("SniperSounds").GetComponent<AudioSource>();

        //soundCollider.enabled = false;
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
        //Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Vector3 rayOrigin = muzzle.transform.position;
        RaycastHit hit;
        Physics.Raycast(rayOrigin,muzzle.forward, out hit, weaponRange + laserRange);
        // If Raycast hits wall

        Vector3 euler = Camera.main.transform.eulerAngles;
        transform.localRotation = Quaternion.Euler(euler.x, 0, 0);
        if (hit.collider)
        {// Rotate gun to hit point - Quaternion.LookRotation(direction)
            //Vector3 relativePos = hit.point - transform.position;
            //transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            laserSight.position = hit.point;


        }
        else
        {
            //Vector3 relativePos = playerCam.transform.forward * laserRange;
            //transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            laserSight.position = playerCam.transform.forward * laserRange;
        }

    }

    // Update is called once per frame
    void Update()
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
        if (Input.GetKey(KeyCode.Mouse0) && currentAmmo <= 0 && remainingAmmo > 0
             && Time.timeScale == 1)
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
        SpawnCollider();
        //Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        Vector3 rayOrigin = muzzle.transform.position;

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, /*playerCam.transform.forward*/ muzzle.forward, out hit, weaponRange))
        {
            Instantiate(bulletParticle, hit.point, Quaternion.identity);
            Vector3 direction = (hit.point - muzzle.position).normalized;
            MinionHealth enemyHealth = hit.collider.GetComponent<MinionHealth>();
            Health hHandler = hit.collider.GetComponent<Health>();
            WeakpointHealth weakpoint = hit.collider.GetComponent<WeakpointHealth>();
            BossHealth bossHealth = hit.collider.GetComponentInParent<BossHealth>();
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
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(2);
            }
        }
        else
        {

        }

    }

    private IEnumerator ShotEffect()
    {
        if (reloading == false)
        {
            soundSource.clip = soundclips[0];
            soundSource.Play();
        }

        yield return shotDuration;


    }
    private IEnumerator ReloadingSequence()
    {
        if (!reloading)
        {
            soundSource.clip = soundclips[1];
            soundSource.Play();
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
    private void OnGUI()
    {
        //float x = (Screen.width / 2) - (crosshair.width*2 / 2);
        //float y = (Screen.height / 2) - (crosshair.height*2 / 2);
        //GUI.DrawTexture(new Rect(x, y, crosshair.width*2, crosshair.height*2),crosshair,ScaleMode.StretchToFill,true,1,Color.red,0,0);
    }

    public void SpawnCollider()
    {
//        Transform soundPosition = gameObject.transform;
//        Instantiate(soundPoint, soundPosition);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 50f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Enemy")
            {
                //BehaviourAI enemy = hitColliders[i].GetComponent<BehaviourAI>();
                //enemy.Investigate(transform.position); // Tell enemy to investigate a position

                AI_ScoutDrone enemy = hitColliders[i].GetComponent<AI_ScoutDrone>();
                enemy.Investigate(transform.position);
                //yield return new WaitForSeconds(5.0f);
                //Physics.IgnoreCollision(enemy.GetComponent<Collider>().hitColliders[i].tag == "Enemy");
                //hitColliders[i].SendMessage("Investigate");
            }
            i++;
        }
    }
}
