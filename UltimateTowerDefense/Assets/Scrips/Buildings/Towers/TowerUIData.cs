using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerUIData", menuName = "TowerUIData")]

public class TowerUIData : ScriptableObject
{
    [Header("Tower UI Data")]
    [SerializeField] private Sprite towerIcon;
    [SerializeField] private string towerName;
    [SerializeField, TextArea(4, 8)] private string towerDescription;
    public Sprite TowerIcon { get { return towerIcon; } }
    public string TowerName { get { return towerName; } }
    public string TowerDescription { get { return towerDescription; } }
}
