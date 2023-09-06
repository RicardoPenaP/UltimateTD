using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManangement : MonoBehaviour
{
    [Header("TileManangement")]
    [SerializeField] private IBuilding tileBuilding;

    private Tile myTile;

    private void Awake()
    {
        myTile = GetComponent<Tile>();
    }

    private void OnMouseDown()
    {
        if (myTile.TileStatus == TileStatusID.Free)
        {
            if (tileBuilding == null)
            {
                if (StoreMananger.Instance.SelectedTower)
                {
                    CreateBuilding();
                }
            }
        }
       
        
    }

    private void CreateBuilding()
    {
        tileBuilding = Instantiate(StoreMananger.Instance.SelectedTower.TowerPrefab, transform.position, Quaternion.identity, transform).GetComponent<IBuilding>();
        StoreMananger.Instance.CreateTower();
    }
}
