using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingInterface;

public class TowerController : MonoBehaviour,IBuilding
{
    [Header("Tower Controller")]
    [SerializeField] private TowerData myData;

    private BuildingInfo myInfo = new BuildingInfo();
    public GameObject AmmunitionPrefab { get { return myData.AmmunitionPrefab; } }
    public int AttackDamage { get { return myInfo.currentAttackDamage; } }
    public float AttackRatio { get { return myInfo.currentAttackRatio; } }
    public float AttackRange { get { return myInfo.currentAttackRange; } }

    //Debugging Tools
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, myData.BaseAttackRange);
    }

    private void Awake()
    {
        SetTowerValues();        
    }

    private void OnMouseDown()
    {
        BuildingMananger.Instance.OpenBuildingMananger(myInfo,myData.TowerDescription,myData.TowerIcon);
    }

    private void SetTowerValues()
    {
        gameObject.name = myData.TowerName;
        myInfo.name = myData.TowerName;        
        myInfo.currentLevel = 1;
        myInfo.currentAttackDamage = myData.BaseAttackDamage;
        myInfo.currentAttackRatio = myData.BaseAttackRatio;
        myInfo.currentAttackRange = myData.BaseAttackRange;
        myInfo.currentUpgradeGoldCost = myData.BaseUpgradeGoldCost;
    }
}
