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

    private Button cleanButton;
    private TextMeshProUGUI obstaclesText;
    private TextMeshProUGUI costText;
    private TileManangement activeTerrain;
    private int cleanCost = 5;


    protected override void Awake()
    {
        base.Awake();
        cleanButton = GetComponentInChildren<Button>();
        obstaclesText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        costText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        cleanButton.onClick.AddListener(Clean);
    }

    private void Start()
    {
        CloseTerrainMananger();
    }

    private void Update()
    {
        UpdateButtonStatus();
    }

    private void OnDestroy()
    {
        cleanButton.onClick.RemoveListener(Clean);
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
        activeTerrain = tile;
        SetCostText();
        SetObstaclesText(tile.AmountOfObstacles);
        gameObject.SetActive(true);
        transform.position = Camera.main.WorldToScreenPoint(tile.transform.position);
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
    private void SetObstaclesText(int obstaclesAmount)
    {
        obstaclesText.text = $"Obstacles: {obstaclesAmount}";
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
