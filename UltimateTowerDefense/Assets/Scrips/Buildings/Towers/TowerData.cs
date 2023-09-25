using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerData",menuName =("TowerData"))]
public class TowerData : ScriptableObject
{
    [Header("Tower Data")]

    [Header("UI Settings")]
    [SerializeField] private BuildingUIData uiData;

    [Header("Leveling Data")]
    [SerializeField] private TowerLevelingData levelingData;

    [Header("Prefab Reference")]    
    [SerializeField] private GameObject ammunitionPrefab;

    [Header("Gameplay Settings")]  
   
    [Tooltip("Amount of damage dealt per attack")]
    [SerializeField, Min(0)] int baseAttackDamage = 0;
    [Tooltip("Amount of attacks per second")]
    [SerializeField, Min(0)] float baseAttackRatio = 0;
    [Tooltip("The range for dectecting enemies and attack them")]
    [SerializeField, Min(0)] float baseAttackRange = 0;

    [Header("Gold Cost Settings")]
    [SerializeField, Min(0)] private int baseLevelUpCost;
    [SerializeField, Range(0, 100)] private int sellValuePercentageCoeficient;

    public BuildingUIData UIData { get { return uiData; } }
    public GameObject AmmunitionPrefab { get { return ammunitionPrefab; } }  
    public int BaseAttackDamage { get { return baseAttackDamage; } }
    public float BaseAttackRatio { get { return baseAttackRatio; } }
    public float BaseAttackRange { get { return baseAttackRange; } }
    public int BaseLevelUpCost { get { return baseLevelUpCost; } }
 
    public float SellValuePercentageCoeficient { get { return sellValuePercentageCoeficient; } }
    public float GetLevelRelatedStatValue(TowerStatToAugment wantedStatValue, int level)
    {
        float statValue = 0f;

        switch (wantedStatValue)
        {
            case TowerStatToAugment.BaseLevelUpGoldCost:
                statValue = baseLevelUpCost;
                break;
            case TowerStatToAugment.BaseAttackDamage:
                statValue = baseAttackDamage;
                break;
            case TowerStatToAugment.BaseAttackRatio:
                statValue = baseAttackRatio;
                break;
            case TowerStatToAugment.BaseAttackRange:
                statValue = baseAttackRange;
                break;
            default:
                break;
        }

        return statValue * levelingData.GetAugmentCostCoeficient(wantedStatValue, level);
    }
}
