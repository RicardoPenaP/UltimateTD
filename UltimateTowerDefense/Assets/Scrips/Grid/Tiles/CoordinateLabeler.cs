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

    private Vector2Int coordinates = new Vector2Int();
    private TextMeshPro labelText;
    private Tile tile;

    private void Awake()
    {   
        labelText = GetComponent<TextMeshPro>();
        tile = GetComponentInParent<Tile>();
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
    }
}
