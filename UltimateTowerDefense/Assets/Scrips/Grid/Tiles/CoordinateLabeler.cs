using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [Header("Labeler")]
    [SerializeField] private int tileSize = 5;
    [SerializeField] private Color freeColor = Color.white;
    [SerializeField] private Color walkableColor = Color.green;
    [SerializeField] private Color occupedColor = Color.red;
    [SerializeField] private Color pathColor = Color.blue;

    private Vector2Int coordinates = new Vector2Int();
    private TextMeshPro labelText;
    private Tile tile;

    private void Awake()
    {   
        labelText = GetComponent<TextMeshPro>();
        tile = GetComponentInParent<Tile>();
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            if (labelText.enabled)
            {
                labelText.enabled = !labelText.enabled;
            }            
        }
    }

    private void Update()
    {  
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleCordinateLabeler();
        }

        DisplayCoordinates();
    }

    private void ToggleCordinateLabeler()
    {
        labelText.enabled = !labelText.enabled;
    }

    private void DisplayCoordinates()
    {
        if (!labelText.enabled)
        {
            return;
        }

        coordinates.x = Mathf.RoundToInt(transform.position.x / tileSize);
        coordinates.y = Mathf.RoundToInt(transform.position.z / tileSize);

        labelText.text = $"({coordinates.x},{coordinates.y})";

        if (tile.isPath)
        {
            labelText.color = pathColor;
            return;
        }

        switch (tile.TileStatus)
        {
            case TileStatusID.Free:
                labelText.color = freeColor;
                break;            
            case TileStatusID.Occuped:
                labelText.color = occupedColor;
                break;
            default:
                break;
        }

       
    }
}
