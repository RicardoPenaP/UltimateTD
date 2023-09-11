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
            PreviewBuildingMananger.Instance.PreviewTower(transform.position);
        }
    }

    private void OnMouseExit()
    {
        if (myTile.TileStatus == TileStatusID.Free)
        {
            PreviewBuildingMananger.Instance.TurnOffPreview();
        }
    }

    private void CreateBuilding()
    {
        tileBuilding = StoreMananger.Instance.CreateBuilding(transform);
        if (tileBuilding == null)
        {
            return;
        }
        myTile.TileStatus = TileStatusID.Occuped;        
    }
}
