using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingInterface;

public class TileManangement : MonoBehaviour
{
    [Header("Tile Manangement")]

    private IBuilding tileBuilding;

    private Tile myTile;

    private OnSellDelegate onSellMethods;

    private void Awake()
    {
        myTile = GetComponent<Tile>();
        onSellMethods = SellBuilding;
    }

    private void OnMouseDown()
    {
        if (PauseMenu.Instance?.IsPaused == true)
        {
            return;
        }
        if (myTile.TileStatus == TileStatusID.Free)
        {
            if (StoreMananger.Instance?.SelectedTower)
            {
                CreateBuilding();
            }
        }
    }

    private void OnMouseEnter()
    {
        if (PauseMenu.Instance?.IsPaused == true)
        {
            return;
        }
        if (myTile.TileStatus == TileStatusID.Free)
        {
            PreviewBuildingMananger.Instance?.PreviewTower(transform.position);
        }
    }

    private void OnMouseExit()
    {
        if (PauseMenu.Instance?.IsPaused == true)
        {
            return;
        }
        if (myTile.TileStatus == TileStatusID.Free)
        {
            PreviewBuildingMananger.Instance?.TurnOffPreviewTower();
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
        tileBuilding.SubscribeToOnSell(onSellMethods);
    }

    private void SellBuilding()
    {   
        tileBuilding = null;
        myTile.TileStatus = TileStatusID.Free;
    }
}
