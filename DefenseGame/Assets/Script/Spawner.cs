﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //limit it to 5 
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {
                if (hit.collider.gameObject.tag == ("MovePoint")) {
                    
                    Instantiate<GameObject>(prefab, (hit.collider.gameObject.transform.position), Quaternion.Euler(-90,0,0));
                }
            }
        }
    }

    
}