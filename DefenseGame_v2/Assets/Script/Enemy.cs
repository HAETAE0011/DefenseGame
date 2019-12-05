using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform startpoint;
    public Transform endpoint;
    public float startSpeed = 0.5f;

    [HideInInspector]
    public float speed;

    [Header("Enemy Set Up")]

    public float totalHealth = 100;
    public float currentHealth;
    public GameObject dieEffect;
    public bool isDead = false;
    public bool isAttack = false;
    public bool isInRange = false;

    [Header("Path Holder")]
    public float waitTime = .3f;
    public float turnSpeed = 90;
    public Transform pathHolder;
    private int wavepointIndex = 0;
    private Transform target;

    public float totaldistance;
    public float remainDistance;
    float distanceTravelled = 0;
    Vector3 lastPosition;

    // Start is called before the first frame update

    void Start()
    {
        currentHealth = totalHealth;
        target = PathHolder.points[0];
        lastPosition = transform.position;

        for (int i = 0; i < PathHolder.points.Length; i++)
        {
            int k = i + 1;

            if (k > 5)
            {
                k = 5;
            }
            else
            {
                float pos = Vector3.Distance(PathHolder.points[i].position, PathHolder.points[k].position);
                totaldistance += pos;
            }
        }
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy Die");
            Die();
        }

        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        remainDistance = totaldistance - distanceTravelled;

        Vector3 dir = target.position - transform.position;

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
            StartCoroutine(TurnToFace(target.position));
        }

        if (isAttack)
        {
            StartCoroutine(RunFaster(1.5f));
        }
        else
        {
            speed = startSpeed;
        }
        
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= PathHolder.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = PathHolder.points[wavepointIndex];
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

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }


    IEnumerator RunFaster(float pct)
    {
        while (isInRange)
        {
            speed = startSpeed * (1f + pct);
            yield return null;
        }

        isAttack = false;
    }

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    //void OnDrawGizmos()
    //{
    //    Vector3 startPosition = pathHolder.GetChild(0).position;
    //    Vector3 previousPosition = startPosition;

    //    foreach (Transform waypoint in pathHolder)
    //    {
    //        Gizmos.DrawSphere(waypoint.position, .3f);
    //        Gizmos.DrawLine(previousPosition, waypoint.position);
    //        previousPosition = waypoint.position;
    //    }
    //    Gizmos.DrawLine(previousPosition, startPosition);
    //}

    void EndPath()
    {
        GameObject[] towerlist = GameObject.FindGameObjectsWithTag("Tower");

        Debug.Log(towerlist.Length);
        int i = Random.Range(0, towerlist.Length);

        if (towerlist.Length >= 1)
        {
            if (towerlist[i] != null)
            {
                Destroy(towerlist[i]);
            }
        }
        else Debug.Log("All Tower Is Destroy");

        Destroy(gameObject);
    }
}
