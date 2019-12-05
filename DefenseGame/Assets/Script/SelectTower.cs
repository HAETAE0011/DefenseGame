using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTower : MonoBehaviour
{
    public TowerBluePrint kingTower;
    public TowerBluePrint queenTower;
    public TowerBluePrint knightTower;
    public TowerBluePrint rookTower;
    public TowerBluePrint bishopTower;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
        buildManager.SelectTowerToBuild(rookTower);
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

    public void SelectRookTower()
    {
        buildManager.SelectTowerToBuild(rookTower);
    }

    public void SelectBishopTower()
    {
        buildManager.SelectTowerToBuild(bishopTower);
    }
}
