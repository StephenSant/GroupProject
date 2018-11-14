using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 450f;
    public float curHealth;
    public bool isAlive;

	// Use this for initialization
	void Start ()
    {
        curHealth = maxHealth;
        isAlive = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0 && isAlive)
        {
            isAlive = !isAlive;
            Dead();
        }
	}
    void Dead()
    {
        gameObject.SetActive(false);
    }
    void TakeDamage()
    {

    }
}
