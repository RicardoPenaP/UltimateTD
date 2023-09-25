using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BuildingInterface;

public class StoreMananger : Singleton<StoreMananger>
{
    [Header("Store Mananger")]
    [SerializeField] private StoreManangerButton buttonPrefab;
    [SerializeField] private List<TowerController> availableTowers;
    

    private Dictionary<TowerController, StoreManangerButton> towerButtons = new Dictionary<TowerController, StoreManangerButton>();

    private TowerController selectedTower;

    public TowerController SelectedTower { get { return selectedTower; } }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SetButtons();
    }

    private void Update()
    {
        CheckForAvailableGold();
        CheckForSelectedButton();
        CancelTowerSelection();
    }
   

    private void SetButtons()
    {
        foreach (TowerController tower in availableTowers)
        {
            AddButton(tower);
        }
    }

    private void AddButton(TowerController tower)
    {
        if (!towerButtons.ContainsKey(tower))
        {
            BuildingUIData uiData = tower.GetBuildindUIInfo();
            StoreManangerButton button = Instantiate(buttonPrefab, transform.position, Quaternion.identity, transform);
            button.SetButtonIcon(uiData.BuildingIcon);
            button.SetGoldCostText(uiData.BaseGoldCost);
            towerButtons.Add(tower, button);
        }
    }

    private void CheckForSelectedButton()
    {
        foreach (KeyValuePair<TowerController, StoreManangerButton> entry in towerButtons)
        {           
            if (entry.Value.ButtonPressed)
            {
                entry.Value.ButtonPressed = false;
                if (selectedTower != entry.Key)
                {
                    selectedTower = entry.Key;
                }
            }
        }
              
    }

    private void CheckForAvailableGold()
    {
        foreach (KeyValuePair<TowerController, StoreManangerButton> entry in towerButtons)
        {
            if (BankMananger.Instance.HaveEnoughGoldCheck(entry.Key.GetBuildindUIInfo().BaseGoldCost))
            {
                entry.Value.SetButtonState(true);
            }
            else
            {
                entry.Value.SetButtonState(false);
            }            
        }       
    }

    private void CancelTowerSelection()
    {
        foreach (KeyValuePair<TowerController, StoreManangerButton> entry in towerButtons)
        {
            if (entry.Value.ButtonSelected)
            {
                return;
            }
        } 
        selectedTower = null;
        PreviewBuildingMananger.Instance.TurnOffPreviewTower();
    }

   
    public IBuilding CreateBuilding(Transform tileTransform)
    {
        if (MouseOverUIMananger.Instance.MouseOverUI)
        {
            return null;
        }
        PreviewBuildingMananger.Instance.TurnOffPreviewTower();
        BankMananger.Instance.SubtractGold(selectedTower.GetBuildindUIInfo().BaseGoldCost);
        IBuilding building = Instantiate(selectedTower, tileTransform.position, Quaternion.identity, tileTransform).GetComponent<IBuilding>();
        selectedTower = null;
        return building;
    }

  

}
