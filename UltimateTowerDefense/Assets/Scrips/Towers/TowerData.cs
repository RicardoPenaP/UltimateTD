using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerData",menuName =("TowerData"))]
public class TowerData : ScriptableObject
{
    [Header("Tower Data")]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private string towerName;
    [SerializeField] private Sprite uiIcon;
    [SerializeField,Min(0)] private int goldCost;

    public GameObject TowerPrefab { get { return towerPrefab; } }
    public string TowerName { get { return towerName; } }
    public Sprite UIIcon { get { return uiIcon; } }
    public int GoldCost { get { return goldCost; } }
}
