using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BuildingInterface;

public class TowerController : MonoBehaviour,IBuilding
{
    [Header("Tower Controller")]
    [SerializeField] private TowerData myData;

    private BuildingInfo myInfo = new BuildingInfo();

    private int totalBuildingCost;

    private OnSellDelegate onSell;

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
        BuildingMananger.Instance.OpenBuildingMananger(this);
    }

    private void SetTowerValues()
    {
        float percentageCalculatorHelper = (myData.SellValuePercentageCoeficient / 100);
        totalBuildingCost = myData.BaseGoldCost;

        gameObject.name = myData.TowerName;
        myInfo.name = myData.TowerName;        
        myInfo.currentLevel = 1;
        myInfo.currentAttackDamage = myData.BaseAttackDamage;
        myInfo.currentAttackRatio = myData.BaseAttackRatio;
        myInfo.currentAttackRange = myData.BaseAttackRange;
        myInfo.currentUpgradeGoldCost = myData.BaseLevelUpGoldCost;
        myInfo.sellCost = Mathf.RoundToInt((float)totalBuildingCost * percentageCalculatorHelper);
       
    }

    //Interface Implementations
    public BuildingInfo GetBuildingInfo()
    {
        return myInfo;
    }
    public Sprite GetBuildingIcon()
    {
        return myData.TowerIcon;
    }
    public string GetBuildingDescription()
    {
        return myData.TowerDescription;
    }
    public void LevelUp()
    {
        myInfo.currentLevel++;        
        myInfo.currentAttackDamage = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(TowerStatToAugment.BaseAttackDamage, myInfo.currentLevel));        
        myInfo.currentAttackRange *= myData.GetLevelRelatedStatValue(TowerStatToAugment.BaseAttackRange, myInfo.currentLevel);
        myInfo.currentAttackRatio *= myData.GetLevelRelatedStatValue(TowerStatToAugment.BaseAttackRatio, myInfo.currentLevel);        
        totalBuildingCost += myInfo.currentUpgradeGoldCost;
        myInfo.currentUpgradeGoldCost = Mathf.RoundToInt(myData.GetLevelRelatedStatValue(TowerStatToAugment.BaseLevelUpGoldCost, myInfo.currentLevel));
        float percentageCalculatorHelper = (myData.SellValuePercentageCoeficient / 100);
        myInfo.sellCost = Mathf.RoundToInt((float)totalBuildingCost * percentageCalculatorHelper);
    }
    public void SellBuilding()
    {
        onSell.Invoke();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public void SubscribeToOnSell(OnSellDelegate onSell)
    {
        this.onSell += onSell;
    }
}
