using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMananger : Singleton<BuildingMananger>
{
    [Header("Building Mananger")]
    [SerializeField] private Button levelUpButton;
    private BuildingManangerIcon buildingIcon;
    private BuildingManangerInfo buildingInfo;

    protected override void Awake()
    {
        base.Awake();

        buildingIcon = GetComponentInChildren<BuildingManangerIcon>();
        buildingInfo = GetComponentInChildren<BuildingManangerInfo>();
    }
}
