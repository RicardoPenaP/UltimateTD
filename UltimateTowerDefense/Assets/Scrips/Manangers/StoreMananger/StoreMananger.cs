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

    private TowerData selectedTower;

    public TowerData SelectedTower { get { return selectedTower; } }

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
            StoreManangerButton button = Instantiate(buttonPrefab, transform.position, Quaternion.identity, transform);
            button.SetButtonIcon(tower.TowerIcon);
            button.SetGoldCostText(tower.BaseGoldCost);
            towerButtons.Add(tower, button);
        }
    }

    private void CheckForSelectedButton()
    {
        foreach (KeyValuePair<TowerData, StoreManangerButton> entry in towerButtons)
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
        foreach (KeyValuePair<TowerData, StoreManangerButton> entry in towerButtons)
        {
            if (BankMananger.Instance.HaveEnoughGoldCheck(entry.Key.BaseGoldCost))
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
        foreach (KeyValuePair<TowerData, StoreManangerButton> entry in towerButtons)
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
        BankMananger.Instance.SubtractGold(selectedTower.BaseGoldCost);
        IBuilding building = Instantiate(selectedTower.TowerPrefab, tileTransform.position, Quaternion.identity, tileTransform).GetComponent<IBuilding>();
        selectedTower = null;
        return building;
    }

  

}
