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
    
    private void Start()
    {
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
                TurnOffPreview();                
            }
            return;
        }
        towerPreview.transform.position = tilePosition;
        towerPreview.SetActive(true);
        rangePreview.transform.position = tilePosition;
        rangePreview.SetCircleSize(StoreMananger.Instance.SelectedTower.BaseAttackRange);
        rangePreview.gameObject.SetActive(true);
    }

    public void TurnOffPreview()
    {
        if (!towerPreview.activeInHierarchy)
        {
            return;
        }
        towerPreview.SetActive(false);
        towerPreview.transform.position = Vector3.zero;
        rangePreview.gameObject.SetActive(false);
        rangePreview.transform.position = Vector3.zero;        
    }

    
}
