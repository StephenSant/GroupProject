using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public float animTime;
    private float timer;

    public float damage;

    // Use this for initialization
    void Start ()
    {
        timer = animTime;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        MinionHealth enemyHealth = other.GetComponent<MinionHealth>();
        Barrel barrelHealth = other.GetComponent<Barrel>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
        if (barrelHealth != null)
        {
            barrelHealth.health -= damage;
        }
        //player and boss
    }
}
