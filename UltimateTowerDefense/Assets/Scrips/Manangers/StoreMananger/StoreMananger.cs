using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreMananger : Singleton<StoreMananger>
{
    [Header("Store Mananger")]
    [SerializeField] private List<TowerData> availableTowers;
    private StoreManangerButton[] buttons;

    private TowerData selectedTower;

    public TowerData SelectedTower { get { return selectedTower; } }

    protected override void Awake()
    {
        base.Awake();
        buttons = new StoreManangerButton[transform.childCount];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = transform.GetChild(i).GetComponent<StoreManangerButton>();
        }        
    }

    private void Start()
    {
        SetGoldCost();        
    }

    private void Update()
    {
        CheckForAvailableGold();
        CheckForSelectedButton();
        CancelTowerSelection();
    }

    private void SetGoldCost()
    {
        if (availableTowers.Count < 1)
        {
            return;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < availableTowers.Count)
            {
                buttons[i].SetGoldCostText(availableTowers[i].GoldCost);
            }
        }
    }

    private void CheckForSelectedButton()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].ButtonPressed)
            {
                buttons[i].ButtonPressed = false;

                if (selectedTower != availableTowers[i])
                {
                    selectedTower = availableTowers[i];
                }                
            }
        }
    }

    private void CheckForAvailableGold()
    {
        for (int i = 0; i < availableTowers.Count; i++)
        {
            if (BankMananger.Instance.HaveEnoughGoldCheck(availableTowers[i].GoldCost))
            {
                buttons[i].SetButtonState(true);
            }
            else
            {
                buttons[i].SetButtonState(false);
            }
        }
    }

    private void CancelTowerSelection()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].ButtonSelected)
            {
                return;
            }
        }

        selectedTower = null;
        PreviewBuildingMananger.Instance.TurnOffPreview();
    }

   
    public IBuilding CreateBuilding(Transform tileTransform)
    {
        PreviewBuildingMananger.Instance.TurnOffPreview();
        BankMananger.Instance.SubtractGold(selectedTower.GoldCost);
        IBuilding building = Instantiate(selectedTower.TowerPrefab, tileTransform.position, Quaternion.identity, tileTransform).GetComponent<IBuilding>();
        selectedTower = null;
        return building;
    }

  

}
