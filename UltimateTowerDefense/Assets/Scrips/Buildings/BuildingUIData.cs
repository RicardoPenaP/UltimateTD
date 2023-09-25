using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingUIData", menuName = "BuildingUIData")]

public class BuildingUIData : ScriptableObject
{
    [Header("Tower UI Data")]
    [SerializeField] private int baseGoldCost;
    [SerializeField] private Sprite buildingIcon;
    [SerializeField] private string buildingName;
    [SerializeField, TextArea(4, 8)] private string buildingDescription;
    public int BaseGoldCost { get { return baseGoldCost; } }
    public Sprite BuildingIcon { get { return buildingIcon; } }
    public string BuildingName { get { return buildingName; } }
    public string BuildingDescription { get { return buildingDescription; } }
}
