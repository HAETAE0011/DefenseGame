using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private TowerBluePrint towerToBuild;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public void SelectTowerToBuild(TowerBluePrint tower)
    {
        towerToBuild = tower;
    }

    public TowerBluePrint GetTowerToBuild()
    {
        return towerToBuild;
    }
}
