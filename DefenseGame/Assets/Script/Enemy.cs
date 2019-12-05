using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform startpoint;
    public Transform endpoint;

    [Header("Enemy Set Up")]

    public float totalHealth = 100;
    public float currentHealth;
    public GameObject dieEffect;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void Die()
    {
        isDead = true;
        GameObject effectIns = (GameObject)Instantiate(dieEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        Destroy(gameObject);
    }

    
}
