using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMananger : Singleton<BuildingMananger>
{

    private BuildingManangerIcon buildingIcon;

    protected override void Awake()
    {
        base.Awake();

        buildingIcon = GetComponentInChildren<BuildingManangerIcon>();
    }
}
