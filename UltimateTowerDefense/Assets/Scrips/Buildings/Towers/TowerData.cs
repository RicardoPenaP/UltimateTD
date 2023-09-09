using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerData",menuName =("TowerData"))]
public class TowerData : ScriptableObject
{
    [Header("Tower Data")]
    [Header("Prefab Reference")]
    [SerializeField] private TowerController towerPrefab;
    [SerializeField] private GameObject ammunitionPrefab;
    [Header("Gameplay Settings")]
    [SerializeField, Min(0)] private int goldCost = 0;
    [Tooltip("Amount of damage dealt per attack")]
    [SerializeField, Min(0)] int attackDamage = 0;
    [Tooltip("Amount of attacks per second")]
    [SerializeField, Min(0)] float attackRatio = 0;
    [Tooltip("The range for dectecting enemies and attack them")]
    [SerializeField, Min(0)] float attackRange = 0;

    [Header("UI Settings")]
    [SerializeField] private Sprite towerIcon;
    [SerializeField] private string towerName;

    public TowerController TowerPrefab { get { return towerPrefab; } }
    public GameObject AmmunitionPrefab { get { return ammunitionPrefab; } }
    public int GoldCost { get { return goldCost; } }
    public int AttackDamage { get { return attackDamage; } }
    public float AttackRatio { get { return attackRatio; } }
    public float AttackRange { get { return attackRange; } }
    public Sprite TowerIcon { get { return towerIcon; } }
    public string TowerName { get { return towerName; } }
   
}
