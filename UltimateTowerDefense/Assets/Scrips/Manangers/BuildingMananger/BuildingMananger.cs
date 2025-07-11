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

    //Setting Methods
    private void SubmitToButtonsEvents()
    {
        closeButton.GetComponent<Button>()?.onClick.AddListener(CloseBuildingManangerWindow);
        levelUpButton.GetComponent<Button>()?.onClick.AddListener(LevelUp);
        sellButton.GetComponent<Button>()?.onClick.AddListener(SellBuilding);
    }

    //Utilities Methods
    public void CloseBuildingManangerWindow()
    {
        PreviewBuildingMananger.Instance?.TurnOffPreviewRange();
        gameObject.SetActive(false);
    }

    public void OpenBuildingMananger(IBuilding selectedBuilding)
    {
        if (MouseOverUIMananger.Instance.MouseOverUI)
        {
            return;
        }
        this.selectedBuilding = selectedBuilding;
        UpdateSelectedBuildingInfo();
        buildingIcon.SetIcon(selectedBuilding.GetBuildindUIInfo().BuildingIcon);        
        gameObject.SetActive(true);
    }

    private void LevelUp()
    {
        if (PauseMenu.Instance?.IsPaused == true)
        {
            return;
        }
        if (selectedBuilding == null)
        {
            return;
        }
        BankMananger.Instance.SubtractGold(selectedBuildingInfo.currentLevelUpGoldCost);
        selectedBuilding.LevelUp();
        UpdateSelectedBuildingInfo();
    }

    private void SellBuilding()
    {
        if (PauseMenu.Instance?.IsPaused == true)
        {
            return;
        }
        if (selectedBuilding == null)
        {
            return;
        }
        BankMananger.Instance.AddGold(selectedBuildingInfo.sellCost);
        selectedBuilding.SellBuilding();
        CloseBuildingManangerWindow();
        selectedBuilding = null;
    }

    //Check and Update Methods

    private void LevelUpAvailableCheck()
    {
        if (selectedBuilding == null)
        {
            return;
        }

        if (BankMananger.Instance.HaveEnoughGoldCheck(selectedBuildingInfo.currentLevelUpGoldCost))
        {
            levelUpButton.SetButtonInteractable(true);
        }
        else
        {
            levelUpButton.SetButtonInteractable(false);
        }
    }

    private void UpdateSelectedBuildingInfo()
    {
        if (selectedBuilding==null)
        {
            return;
        }
        selectedBuildingInfo = selectedBuilding.GetBuildingInfo();
        this.buildingInfo.SetName(selectedBuildingInfo.name);
        this.buildingInfo.SetLevel(selectedBuildingInfo.currentLevel);
        this.buildingInfo.SetBuildingDescription(selectedBuilding.GetBuildindUIInfo().BuildingDescription);
        this.buildingInfo.SetBuildingDamage(selectedBuildingInfo.currentAttackDamage);
        this.buildingInfo.SetBuildingAttackRatio(selectedBuildingInfo.currentAttackRatio);
        this.buildingInfo.SetBuildingRange(selectedBuildingInfo.currentAttackRange);
        levelUpButton.SetLevelUPCost(selectedBuildingInfo.currentLevelUpGoldCost);
        sellButton.SetSellCost(selectedBuildingInfo.sellCost);
        UpdatePreviewRange();

    }

    private void UpdatePreviewRange()
    {
        PreviewBuildingMananger.Instance.PreviewRange((selectedBuilding as MonoBehaviour).transform.position, selectedBuildingInfo.currentAttackRange);
    }
}
