using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreMananger : Singleton<StoreMananger>
{
    [Header("Store Mananger")]
    [SerializeField] private List<TowerData> availableTowers;
    [SerializeField] private GameObject previewTowerPrefab;
   
    private StoreManangerButton[] buttons;

    private TowerData selectedTower;

    private GameObject towerPreview;
    private bool towerPreviewed = false;

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
        SetPreviewTower();
    }

    private void Update()
    {
        CheckForAvailableGold();
        CheckButtonsPressed();        
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

    private void CheckButtonsPressed()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].ButtonPressed)
            {
                buttons[i].ButtonPressed = false;
                if (i < availableTowers.Count)
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

    private void SetPreviewTower()
    {
        
    }

    public void CreateTower()
    {
        BankMananger.Instance.SubtractGold(selectedTower.GoldCost);
        selectedTower = null;
    }

    public void PreviewTower(Vector3 tilePosition)
    {

    }

}
