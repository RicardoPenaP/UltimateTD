using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TerrainMananger : Singleton<TerrainMananger>
{
    private Button cleanButton;
    private TextMeshProUGUI obstaclesText;
    private TextMeshProUGUI costText;
    private Tile activeTerrain;
    private int cleanCost = 5;


    protected override void Awake()
    {
        base.Awake();
        cleanButton = GetComponentInChildren<Button>();
        obstaclesText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        costText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    public void OpenMenu(Tile tile)
    {
        gameObject.SetActive(true);

        transform.position = Camera.main.WorldToScreenPoint(tile.transform.position);
    }

    public void CloseMenu()
    {

    }

}
