using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNext : MonoBehaviour
{
    public Transform nextpoint;

    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<Enemy>().target = nextpoint;
            Debug.Log("t");
        }
        
    }
}
