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
    BuildingInfo selectedBuildingInfo;
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

    private void Update()
    {
        LevelUpAvailableCheck();
    }

    //Settings Methods
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

    //Utilities Methods
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

    //Check and Update Methods

    private void LevelUpAvailableCheck()
    {
        if (selectedBuilding == null)
        {
            return;
        }

        if (BankMananger.Instance.HaveEnoughGoldCheck(selectedBuildingInfo.currentUpgradeGoldCost))
        {
            levelUpButton.SetButtonInteractable(true);
        }
        else
        {
            levelUpButton.SetButtonInteractable(false);
        }
    }

    public void OpenBuildingMananger(IBuilding selectedBuilding)
    {
        this.selectedBuilding = selectedBuilding;
        selectedBuildingInfo = selectedBuilding.GetBuildingInfo();

        buildingIcon.SetIcon(selectedBuilding.GetBuildingIcon());
        this.buildingInfo.SetName(selectedBuildingInfo.name);
        this.buildingInfo.SetLevel(selectedBuildingInfo.currentLevel);
        this.buildingInfo.SetBuildingDescription(selectedBuilding.GetBuildingDescription());
        this.buildingInfo.SetBuildingDamage(selectedBuildingInfo.currentAttackDamage);
        this.buildingInfo.SetBuildingAttackRatio(selectedBuildingInfo.currentAttackRatio);
        this.buildingInfo.SetBuildingRange(selectedBuildingInfo.currentAttackRange);
        levelUpButton.SetLevelUPCost(selectedBuildingInfo.currentUpgradeGoldCost);
        sellButton.SetSellCost(selectedBuildingInfo.sellCost);
        gameObject.SetActive(true);
    }
}
