using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float animTime;
    private float timer;

    public float damage;

    public float explosionForce=10;
    public float explosionSize=4;

    // Use this for initialization
    void Start()
    {
        timer = animTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {

            if (gameObject.transform.parent == null)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Health hHandler = other.GetComponent<Health>();
        if (hHandler != null)
        {
            hHandler.TakeDamage(damage);
        }
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if(rigidbody != null)
            {
            rigidbody.AddExplosionForce(explosionForce, transform.position, explosionSize);
        }
    }
}
