using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float curHealth;
    public float maxHealth = 100f;

    // Use this for initialization
    void Start ()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        curHealth -= damage;
    }

    // Update is called once per frame
    void Update ()
    {
        if (curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }
    }
}
