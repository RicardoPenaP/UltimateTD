using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BuildingInterface;

public class BuildingMananger : Singleton<BuildingMananger>
{   
    private BuildingManangerIcon buildingIcon;
    private BuildingManangerInfo buildingInfo;
    private Button levelUpButton;
    private Button closeButton;

    protected override void Awake()
    {
        base.Awake();

        buildingIcon = GetComponentInChildren<BuildingManangerIcon>();
        buildingInfo = GetComponentInChildren<BuildingManangerInfo>();
        levelUpButton = GetComponentInChildren<BuildingManangerLevelUpButton>().GetComponent<Button>();
        closeButton = GetComponentInChildren<BuildinManangerCloseButton>().GetComponent<Button>();
        SubmitToButtonsEvents();
    }

    private void Start()
    {
        CloseBuildingManangerWindow();
    }

    private void SubmitToButtonsEvents()
    {
        levelUpButton.onClick.AddListener(LevelUp);
        closeButton.onClick.AddListener(CloseBuildingManangerWindow);
    }

    private void LevelUp()
    {

    }

    private void CloseBuildingManangerWindow()
    {
        gameObject.SetActive(false);
    }

    public void OpenBuildingMananger(BuildingInfo buildingInfo,string buildingDescription, Sprite buildingIcon)
    {
        this.buildingIcon.SetIcon(buildingIcon);
        this.buildingInfo.SetName(buildingInfo.name);
        this.buildingInfo.SetLevel(buildingInfo.currentLevel);
        this.buildingInfo.SetBuildingDescription(buildingDescription);
        this.buildingInfo.SetBuildingDamage(buildingInfo.currentAttackDamage);
        this.buildingInfo.SetBuildingAttackRatio(buildingInfo.currentAttackRatio);
        this.buildingInfo.SetBuildingRange(buildingInfo.currentAttackRange);
        gameObject.SetActive(true);

    }
}
