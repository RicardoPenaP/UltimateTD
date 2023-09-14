using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingInterface;

public class TowerController : MonoBehaviour,IBuilding
{
    [Header("Tower Controller")]
    [SerializeField] private TowerData myData;

    private BuildingStats myStats = new BuildingStats();
    public GameObject AmmunitionPrefab { get { return myData.AmmunitionPrefab; } }
    public int AttackDamage { get { return myStats.currentAttackDamage; } }
    public float AttackRatio { get { return myStats.currentAttackRatio; } }
    public float AttackRange { get { return myStats.currentAttackRange; } }

    private void Awake()
    {
        SetTowerValues();        
    }
    //Debugging Tools
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, myData.BaseAttackRange);
    }

    private void SetTowerValues()
    {
        gameObject.name = myData.TowerName;
        myStats.currentLevel = 1;
        myStats.currentAttackDamage = myData.BaseAttackDamage;
        myStats.currentAttackRatio = myData.BaseAttackRatio;
        myStats.currentAttackRange = myData.BaseAttackRange;
        myStats.currentUpgradeGoldCost = myData.BaseUpgradeGoldCost;
    }
}
