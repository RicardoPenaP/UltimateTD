using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVFX : MonoBehaviour
{
    [Header("Tile VFX")]
    [SerializeField] private ParticleSystem towerCreationVFX;
    [SerializeField] private ParticleSystem towerSellVFX;

    private TileManangement myTileMananger;

    private void Awake()
    {
        myTileMananger = GetComponentInParent<TileManangement>();
        if (!myTileMananger)
        {
            return;
        }
        myTileMananger.OnCleanObstacles += () => towerCreationVFX?.Play();
        myTileMananger.OnCreateBuilding += () => towerCreationVFX?.Play();
        myTileMananger.OnSellBuilding += () => towerSellVFX?.Play();
    }

    private void OnDestroy()
    {
        if (!myTileMananger)
        {
            return;
        }
        myTileMananger.OnCleanObstacles -= () => towerCreationVFX?.Play();
        myTileMananger.OnCreateBuilding -= () => towerCreationVFX?.Play();
        myTileMananger.OnSellBuilding -= () => towerSellVFX?.Play();
    }

}
