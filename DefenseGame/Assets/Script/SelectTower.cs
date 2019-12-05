using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTower : MonoBehaviour
{
    public TowerBluePrint kingTower;
    public TowerBluePrint queenTower;
    public TowerBluePrint knightTower;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectKingTower()
    {
        buildManager.SelectTowerToBuild(kingTower);
    }

    public void SelectQueenTower()
    {
        buildManager.SelectTowerToBuild(queenTower);
    }

    public void SelectKnightTower()
    {
        buildManager.SelectTowerToBuild(knightTower);
    }
}
