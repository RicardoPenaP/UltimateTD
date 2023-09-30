using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerStatToAugment { BaseLevelUpGoldCost, BaseAttackDamage, BaseAttackRatio, BaseAttackRange}
[CreateAssetMenu(fileName = "NewTowerLevelingData", menuName = "TowerLevelingData")]
public class TowerLevelingData : ScriptableObject
{
    [Header("Tower Leveling Data")]
    [Header("Percentage Augment Per Level")]

    [Header("Gold Cost")]
    [SerializeField, Range(0, 100)] private int baseLevelUpGoldCostAugment = 0;

    [Header("Attack stats")]
    [SerializeField, Range(0, 100)] private int baseAttackDamageAugment = 0;
    [SerializeField, Range(0, 100)] private int baseAttackRatioAugment = 0;
    [SerializeField, Range(0, 100)] private int baseAttackRangeAugment = 0;

    public float GetAugmentCostCoeficient(TowerStatToAugment wantedStatCoeficient)
    {
        float resultingPercentage = 0f;

        switch (wantedStatCoeficient)
        {
            case TowerStatToAugment.BaseLevelUpGoldCost:
                resultingPercentage = (float)baseLevelUpGoldCostAugment / 100;
                break;
            case TowerStatToAugment.BaseAttackDamage:
                resultingPercentage = (float)baseAttackDamageAugment / 100;
                break;
            case TowerStatToAugment.BaseAttackRatio:
                resultingPercentage = (float)baseAttackRatioAugment / 100;
                break;
            case TowerStatToAugment.BaseAttackRange:
                resultingPercentage = (float)baseAttackRangeAugment / 100;
                break;
            default:
                break;
        }        
        float augmentCoeficient = 1 + resultingPercentage;

        return augmentCoeficient;
    }
}
