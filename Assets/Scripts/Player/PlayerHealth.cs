using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float curHealth;
    public bool isDead;
    public bool isRegen;
    public float healthRegen = 1;
    public Slider healthBar;
    // Use this for initialization
    void Start()
    {
        curHealth = maxHealth;
        isDead = false;
        healthBar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = curHealth;
        if (curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0 && !isDead)
        {
            
            Dead();
        }
        if (curHealth != maxHealth && !isRegen)
        {
            StartCoroutine(RegenerateHealth());
        }
    }
    public IEnumerator RegenerateHealth()
    {
        isRegen = true;
        while (curHealth < maxHealth)
        {
            RegenHealth();
            yield return new WaitForSeconds(0.5f);

        }
        isRegen = false;
    }
    public void RegenHealth()
    {
        curHealth += healthRegen;
    }
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
    }
    public void Dead()
    {
        isDead = true;
        gameObject.SetActive(false);
    }

    private void OnGUI()
    {
        if (isDead)
        {
            GUI.Box(new Rect(Screen.width * .4f, Screen.height * .5f, Screen.width * .2f, Screen.height * .1f),"Dead");
            Debug.Log("YEET");
        }
    }
}
