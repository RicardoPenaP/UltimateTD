using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    [Header("Tile")]
    [SerializeField] private int tileSize = 5;

    private Vector2Int tileCoordinates = new Vector2Int();

    private void Update()
    {
        tileCoordinates.x = Mathf.RoundToInt(transform.position.x / tileSize);
        tileCoordinates.y = Mathf.RoundToInt(transform.position.z / tileSize);
    }
}
