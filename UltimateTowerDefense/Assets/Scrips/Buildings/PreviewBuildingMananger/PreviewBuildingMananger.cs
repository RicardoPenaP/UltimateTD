using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBuildingMananger : Singleton<PreviewBuildingMananger>
{
    [Header("Preview Building Mananger")]
    [SerializeField] private GameObject previewTowerPrefab;
    [SerializeField] private PreviewRange previewRangePrefab;

    private GameObject towerPreview;
    private PreviewRange rangePreview;

    protected override void Awake()
    {
        base.Awake();
        SetPreviewBuildings();
    }
   
    private void SetPreviewBuildings()
    {
        towerPreview = Instantiate(previewTowerPrefab, transform.position, Quaternion.identity, transform);
        towerPreview.SetActive(false);

        rangePreview = Instantiate(previewRangePrefab, transform.position, Quaternion.identity, transform);
        rangePreview.gameObject.SetActive(false);
    }

    public void PreviewTower(Vector3 tilePosition)
    {
        if (!StoreMananger.Instance.SelectedTower)
        {
            if (towerPreview.activeInHierarchy)
            {
                TurnOffPreviewTower();                
            }
            return;
        }
        towerPreview.transform.position = tilePosition;
        towerPreview.SetActive(true);
        PreviewRange(tilePosition, StoreMananger.Instance.SelectedTower.BaseAttackRange);
       
    }

    public void TurnOffPreviewTower()
    {
        if (!towerPreview.activeInHierarchy)
        {
            return;
        }
        towerPreview.SetActive(false);
        towerPreview.transform.position = Vector3.zero;
        TurnOffPreviewRange();
    }

    public void PreviewRange(Vector3 tilePosition,float rangeSize)
    {
        rangePreview.transform.position = tilePosition;
        rangePreview.SetCircleSize(rangeSize);
        rangePreview.gameObject.SetActive(true);
    }

    public void TurnOffPreviewRange()
    {        
        rangePreview.gameObject.SetActive(false);
        rangePreview.transform.position = Vector3.zero;
    }

    
}
