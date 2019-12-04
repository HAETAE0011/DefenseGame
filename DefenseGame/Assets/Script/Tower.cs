﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public float rangeRadius = 15f;
    public float turnSpeed = 10f;
    public Transform mesh;

    public Transform firePoint;

    [Header("Use for Shoot")]

    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Map")]

    public GameObject endPoint;


    void Start()
    {
        InvokeRepeating("UpdateEnemy", 0f, 0.5f);
    }


    void UpdateEnemy()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject shootEnemy = null;

        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            //Check if enemy in the attack range? 

            //if in the range, check if the enemy is the closest to the end point

            //float closesttoEndpoint = Vector3.Distance(enemy.transform.position, endPoint.transform.position);

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy; 
                shootEnemy = enemy;
            }
        }

        if (shootEnemy != null && shortestDistance <= rangeRadius)
        {
            Debug.Log("Test");
            target = shootEnemy.transform;
            targetEnemy = shootEnemy.GetComponent<Enemy>();
        }

        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        LockOnTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(mesh.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        Debug.Log(rotation);

        mesh.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }

}
