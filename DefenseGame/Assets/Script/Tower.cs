using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private Transform target;
    [SerializeField]
    private Enemy targetEnemy;

    [Header("General")]

    public float rangeRadius = 15f;
    public float turnSpeed = 10f;
    public Transform mesh;
    public GameObject dieEffect;

    [Header("Upgrade")]
    public int kill;
    public GameObject upgrade;
    public bool isUpgraded = false;
    public Material upGradeMat;

    public Transform firePoint;

    [Header("Use for Shoot")]

    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Map")]
    [SerializeField]
    private GameObject endPoint;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int damageOverTime = 30;
    public float slowAmount = .5f;


    void Start()
    {
        kill = 0;
        endPoint = GameObject.FindGameObjectWithTag("EndPoint");
        InvokeRepeating("UpdateEnemy", 0f, 0.5f);
    }


    void UpdateEnemy()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject shootEnemy = null;

        float shortestDistance = Mathf.Infinity;
        float towertoEnemy = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float closesttoEndpoint = Vector3.Distance(enemy.transform.position, endPoint.transform.position);

            float towertoEnemynear = Vector3.Distance(transform.position, enemy.transform.position);

            if (closesttoEndpoint < shortestDistance)
            {
                towertoEnemy = towertoEnemynear;
                shortestDistance = closesttoEndpoint; 
                shootEnemy = enemy;
            }
        }


        if (shootEnemy != null && towertoEnemy <= rangeRadius)
        {
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
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

        if (target.GetComponent<Enemy>().isDead)
        {
            kill++;
            Debug.Log(kill);
        }

        if (kill >= 2 && isUpgraded == false)
        {
            towerUpgrate();
            isUpgraded = true;
        }

        fireCountdown -= Time.deltaTime;
    }

    void towerUpgrate()
    {
        GameObject effectIns = (GameObject)Instantiate(upgrade, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
        gameObject.GetComponentInChildren<Renderer>().material = upGradeMat;

        bulletPrefab.GetComponent<Bullet>().damage = 50;

        //Destroy(gameObject);
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(mesh.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        mesh.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }


    public void DestroySelf() {
        GameObject effectIns = (GameObject)Instantiate(dieEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
        Destroy(gameObject);
    }
}
