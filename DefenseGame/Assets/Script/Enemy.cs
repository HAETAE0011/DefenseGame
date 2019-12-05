using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform startpoint;
    public Transform target;
    
    public Transform endpoint;
    public float startSpeed = 8f;

    [HideInInspector]
    public float speed;

    [Header("Enemy Set Up")]

    public float totalHealth = 100;
    public float currentHealth;
    public GameObject dieEffect;
    public bool isDead = false;

    public GameObject[] towers;

    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
        currentHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if (target.position == endpoint.position) {
            if (transform.position == target.position) {
                Die();
                destroyTower();
            }
        }

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

    public void destroyTower() {
        
        towers = GameObject.FindGameObjectsWithTag("Tower");
        

        towers[Random.Range(0, towers.Length - 1)].GetComponent<Tower>().DestroySelf();
        

    }
    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

}
