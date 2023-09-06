using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManangement : MonoBehaviour
{
    [Header("TileManangement")]
    [SerializeField] private IBuilding tileBuilding;

    private void OnMouseDown()
    {
        if (StoreMananger.Instance.SelectedTower)
        {
            CreateBuilding();
        }
    }

    private void CreateBuilding()
    {
        StoreMananger.Instance.CreateTower();

    }
}
