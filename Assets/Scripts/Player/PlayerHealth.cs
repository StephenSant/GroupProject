using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float curHealth;
    public bool isDead;

    // Use this for initialization
    void Start()
    {
        curHealth = maxHealth;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0 && !isDead)
        {
            isDead = true;
            Dead();
        }

    }
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
    }
    public void Dead()
    {
        
        gameObject.SetActive(false);
    }
}
