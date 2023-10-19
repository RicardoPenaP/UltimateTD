using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TerrainMananger : Singleton<TerrainMananger>
{
    [Header("Terrain mananger")]
    [SerializeField, Min(0)] private float cleanCostAugmentMultiplier = 1;
    [SerializeField,Min(0)] private int baseCleanCost = 5;
    [SerializeField] private Vector3 panelOffset = Vector3.zero;

    private Button cleanButton;
    private Button closeButton;
    private TextMeshProUGUI costText;
    private TileManangement activeTerrain;    
    private int cleanCost;


    protected override void Awake()
    {
        base.Awake();
             
        costText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        cleanButton = transform.GetChild(2).GetComponent<Button>();
        closeButton = transform.GetChild(3).GetComponent<Button>();
        cleanButton.onClick.AddListener(Clean);
        closeButton.onClick.AddListener(CloseTerrainMananger);
    }

    private void Start()
    {
        cleanCost = baseCleanCost;
        CloseTerrainMananger();
    }

    private void Update()
    {
        LocatePanelInScreenSpace();
        UpdateButtonStatus();
    }

    private void OnDestroy()
    {
        cleanButton.onClick.RemoveListener(Clean);
        closeButton.onClick.RemoveListener(CloseTerrainMananger);
    }

    private void UpdateButtonStatus()
    {
        if (BankMananger.Instance.HaveEnoughGoldCheck(cleanCost))
        {
            if (!cleanButton.IsInteractable())
            {
                cleanButton.interactable = true;
            }
        }
        else
        {
            if (cleanButton.IsInteractable())
            {
                cleanButton.interactable = false;
            }
        }

    }

    public void OpenTerrainMananger(TileManangement tile)
    {
        if (MouseOverUIMananger.Instance.MouseOverUI)
        {
            return;
        }

        if (activeTerrain != tile)
        {
            activeTerrain = tile;
        }

        if (!gameObject.activeInHierarchy)
        {
            LocatePanelInScreenSpace();
            gameObject.SetActive(true);
        }

       

        SetCostText();      
       
    }

    private void LocatePanelInScreenSpace()
    {
        if (!activeTerrain)
        {
            return;
        }
        transform.position = Camera.main.WorldToScreenPoint(activeTerrain.transform.position) + panelOffset;
    }

    public void CloseTerrainMananger()
    {
        if (gameObject.activeInHierarchy)
        {
            activeTerrain = null;
            gameObject.SetActive(false);
        }
       
    }

    private void SetCostText()
    {
        costText.text = $"Clean Cost: {cleanCost}";
    }    

    private void Clean()
    {
        BankMananger.Instance.SubtractGold(cleanCost);
        activeTerrain?.CleanObstacles();
        AugmentCleanCost();
        CloseTerrainMananger();
    }

    private void AugmentCleanCost()
    {
        cleanCost = Mathf.RoundToInt(cleanCost * cleanCostAugmentMultiplier);
    }
}
