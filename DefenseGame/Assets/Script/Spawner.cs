using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject tower;
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject.tag == ("MovePoint")) {

                    //Instantiate<GameObject>(tower, (hit.collider.gameObject.transform.position), Quaternion.identity);
                    BuildTurret(buildManager.GetTowerToBuild());
                }
            }
        }
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }

    void BuildTurret(TowerBluePrint blueprint)
    {

        GameObject _tower = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        Debug.Log("Tower build!");
    }



}
