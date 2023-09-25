using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerData",menuName =("TowerData"))]
public class TowerData : ScriptableObject
{
    [Header("Tower Data")]
    [Header("Leveling Data")]
    [SerializeField] private TowerLevelingData levelingData;
    [Header("Prefab Reference")]
    [SerializeField] private TowerController towerPrefab;
    [SerializeField] private GameObject ammunitionPrefab;

    [Header("Gameplay Settings")]  
    [SerializeField, Min(0)] private int baseGoldCost = 0;
    [Tooltip("Amount of damage dealt per attack")]
    [SerializeField, Min(0)] int baseAttackDamage = 0;
    [Tooltip("Amount of attacks per second")]
    [SerializeField, Min(0)] float baseAttackRatio = 0;
    [Tooltip("The range for dectecting enemies and attack them")]
    [SerializeField, Min(0)] float baseAttackRange = 0;

    [Header("Upgrade Settings")]
    [SerializeField, Min(0)] private int baseLevelUpGoldCost;
    [SerializeField, Range(0, 100)] private int upgradeCostAugmentPercentage;
    [SerializeField, Range(0, 100)] private int upgradeStatsAugmentPercentage;
    [SerializeField, Range(0, 100)] private int sellValuePercentageCoeficient;

    [Header("UI Settings")]
    [SerializeField] private Sprite towerIcon;
    [SerializeField] private string towerName;
    [SerializeField,TextArea(4,8)] private string towerDescription;

    public TowerController TowerPrefab { get { return towerPrefab; } }
    public GameObject AmmunitionPrefab { get { return ammunitionPrefab; } }    
    public int BaseGoldCost { get { return baseGoldCost; } }
    public int BaseAttackDamage { get { return baseAttackDamage; } }
    public float BaseAttackRatio { get { return baseAttackRatio; } }
    public float BaseAttackRange { get { return baseAttackRange; } }
    public int BaseLevelUpGoldCost { get { return baseLevelUpGoldCost; } }
    public float UpgradeCostAugmentPercentage { get { return upgradeCostAugmentPercentage; } }
    public float UpgradeStatsAugmentPercentage { get { return upgradeStatsAugmentPercentage; } }
    public float SellValuePercentageCoeficient { get { return sellValuePercentageCoeficient; } }
    public Sprite TowerIcon { get { return towerIcon; } }
    public string TowerName { get { return towerName; } }
    public string TowerDescription { get { return towerDescription; } }
   
    public float GetLevelRelatedStatValue(TowerStatToAugment wantedStatValue, int level)
    {
        float statValue = 0f;

        switch (wantedStatValue)
        {
            case TowerStatToAugment.BaseLevelUpGoldCost:
                statValue = baseLevelUpGoldCost;
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
