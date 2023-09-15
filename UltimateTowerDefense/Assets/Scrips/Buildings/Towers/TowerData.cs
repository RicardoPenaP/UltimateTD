using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerData",menuName =("TowerData"))]
public class TowerData : ScriptableObject
{
    [Header("Tower Data")]
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
    [SerializeField, Min(0)] private int baseUpgradeGoldCost;
    [SerializeField, Range(0, 100)] private int upgradeCostAugmentPercentage;
    [SerializeField, Range(0, 100)] private int upgradeStatsAugmentPercentage;

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
    public int BaseUpgradeGoldCost { get { return baseUpgradeGoldCost; } }
    public float UpgradeCostAugmentPercentage { get { return upgradeCostAugmentPercentage; } }
    public float UpgradeStatsAugmentPercentage { get { return upgradeStatsAugmentPercentage; } }
    public Sprite TowerIcon { get { return towerIcon; } }
    public string TowerName { get { return towerName; } }
    public string TowerDescription { get { return towerDescription; } }
   
}
