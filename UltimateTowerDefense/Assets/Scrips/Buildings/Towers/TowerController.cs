using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BuildingInterface;
using System;

public class TowerController : MonoBehaviour,IBuilding
{
    [Header("Tower Controller")]
    [SerializeField] private TowerData myData;

    private OnSellDelegate OnSell;
    public Action OnLevelUp;

    private BuildingInfo myInfo = new BuildingInfo();

    private int totalBuildingCost;

    public GameObject AmmunitionPrefab { get { return myData.AmmunitionPrefab; } }
    public int AttackDamage { get { return myInfo.currentAttackDamage; } }
    public float AttackRatio { get { return myInfo.currentAttackRatio; } }
    public float AttackRange { get { return myInfo.currentAttackRange; } }

    public float BaseAttackRange { get { return myData.BaseAttackRange; } }

    //Debugging Tools
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, myData.BaseAttackRange);
    }

    private void Awake()
    {
        InitTowerStats();        
    }

    private void OnMouseDown()
    {
        if (PauseMenu.Instance?.IsPaused == true)
        {
            return;
        }

        if (BuildingMananger.Instance)
        {
            BuildingMananger.Instance?.OpenBuildingMananger(this);
        }
       
    }

    private void InitTowerStats()
    {
        float percentageCalculatorHelper = (myData.SellValuePercentageCoeficient / 100);
        totalBuildingCost = myData.UIData.BaseGoldCost;

        gameObject.name = myData.UIData.BuildingName;
        myInfo.name = myData.UIData.BuildingName;
        myInfo.currentLevel = 1;
        myInfo.currentAttackDamage = myData.BaseAttackDamage;
        myInfo.currentAttackRatio = myData.BaseAttackRatio;
        myInfo.currentAttackRange = myData.BaseAttackRange;
        myInfo.currentLevelUpGoldCost = myData.BaseLevelUpCost;
        myInfo.sellCost = Mathf.RoundToInt((float)totalBuildingCost * percentageCalculatorHelper);
       
    }

    //Interface Implementations
    public BuildingInfo GetBuildingInfo()
    {
        return myInfo;
    }
   
    public void LevelUp()
    {
        myInfo.currentLevel++;        
        myInfo.currentAttackDamage = Mathf.RoundToInt(myData.GetStatValueForALevel(TowerStatToAugment.BaseAttackDamage, myInfo.currentLevel));        
        myInfo.currentAttackRange = myData.GetStatValueForALevel(TowerStatToAugment.BaseAttackRange, myInfo.currentLevel);
        myInfo.currentAttackRatio = myData.GetStatValueForALevel(TowerStatToAugment.BaseAttackRatio, myInfo.currentLevel);        
        totalBuildingCost += myInfo.currentLevelUpGoldCost;
        myInfo.currentLevelUpGoldCost = Mathf.RoundToInt(myData.GetStatValueForALevel(TowerStatToAugment.BaseLevelUpGoldCost, myInfo.currentLevel));
        float percentageCalculatorHelper = (myData.SellValuePercentageCoeficient / 100);
        myInfo.sellCost = Mathf.RoundToInt((float)totalBuildingCost * percentageCalculatorHelper);
    }

    public void SellBuilding()
    {
        OnSell.Invoke();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void SubscribeToOnSell(OnSellDelegate onSell)
    {
        this.OnSell += onSell;
    }
    
    public BuildingUIData GetBuildindUIInfo()
    {
        return myData.UIData;
    }
}
