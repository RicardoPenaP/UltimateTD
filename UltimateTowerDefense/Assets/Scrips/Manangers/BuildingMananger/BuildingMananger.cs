using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BuildingInterface;

public class BuildingMananger : Singleton<BuildingMananger>
{   
    private BuildingManangerIcon buildingIcon;
    private BuildingManangerInfo buildingInfo;
    private BuildinManangerCloseButton closeButton;
    private BuildingManangerLevelUpButton levelUpButton;
    private BuildingManangerSellButton sellButton;
    


    private IBuilding selectedBuilding;

    protected override void Awake()
    {
        base.Awake();

        buildingIcon = GetComponentInChildren<BuildingManangerIcon>();
        buildingInfo = GetComponentInChildren<BuildingManangerInfo>();
        levelUpButton = GetComponentInChildren<BuildingManangerLevelUpButton>();
        closeButton = GetComponentInChildren<BuildinManangerCloseButton>();
        sellButton = GetComponentInChildren<BuildingManangerSellButton>();

        SubmitToButtonsEvents();
    }

    private void Start()
    {
        CloseBuildingManangerWindow();
    }

    private void SubmitToButtonsEvents()
    {
        closeButton.GetComponent<Button>()?.onClick.AddListener(CloseBuildingManangerWindow);
        levelUpButton.GetComponent<Button>()?.onClick.AddListener(LevelUp);
        sellButton.GetComponent<Button>()?.onClick.AddListener(SellBuilding);
    }

    private void CloseBuildingManangerWindow()
    {
        gameObject.SetActive(false);
    }

    private void LevelUp()
    {
        if (selectedBuilding == null)
        {
            return;
        }

    }

    private void SellBuilding()
    {

    }

   

    public void OpenBuildingMananger(IBuilding selectedBuilding)
    {
        this.selectedBuilding = selectedBuilding;
        BuildingInfo buildingInfo = selectedBuilding.GetBuildingInfo();

        this.buildingIcon.SetIcon(selectedBuilding.GetBuildingIcon());
        this.buildingInfo.SetName(buildingInfo.name);
        this.buildingInfo.SetLevel(buildingInfo.currentLevel);
        this.buildingInfo.SetBuildingDescription(selectedBuilding.GetBuildingDescription());
        this.buildingInfo.SetBuildingDamage(buildingInfo.currentAttackDamage);
        this.buildingInfo.SetBuildingAttackRatio(buildingInfo.currentAttackRatio);
        this.buildingInfo.SetBuildingRange(buildingInfo.currentAttackRange);
        gameObject.SetActive(true);
    }
}
