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
            return;
        }

        LockOnTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        if (target.GetComponent<Enemy>().isDead)
        {
            kill++;
            Debug.Log(kill);
        }

        if (kill >= 5 && isUpgraded == false)
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


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }

}
