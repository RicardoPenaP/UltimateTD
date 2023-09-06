using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManangement : MonoBehaviour
{
    [Header("TileManangement")]

    private IBuilding tileBuilding;

    private Tile myTile;

    private void Awake()
    {
        myTile = GetComponent<Tile>();
    }

    private void OnMouseDown()
    {
        if (myTile.TileStatus == TileStatusID.Free)
        {
            if (StoreMananger.Instance.SelectedTower)
            {
                CreateBuilding();
            }
        }      
        
    }

    private void OnMouseEnter()
    {
        if (myTile.TileStatus == TileStatusID.Free)
        {
            if (StoreMananger.Instance.SelectedTower)
            {
                
            }
        }
    }

    private void CreateBuilding()
    {
        tileBuilding = Instantiate(StoreMananger.Instance.SelectedTower.TowerPrefab, transform.position, Quaternion.identity, transform).GetComponent<IBuilding>();
        myTile.TileStatus = TileStatusID.Occuped;
        StoreMananger.Instance.CreateTower();
    }
}
