using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBuildingMananger : Singleton<PreviewBuildingMananger>
{
    [Header("Preview Building Mananger")]
    [SerializeField] private GameObject previewTowerPrefab;

    private GameObject towerPreview;
    
    private void Start()
    {
        SetPreviewBuildings();
    }

    private void SetPreviewBuildings()
    {
        towerPreview = Instantiate(previewTowerPrefab, transform.position, Quaternion.identity, transform);
        towerPreview.SetActive(false);
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
    }

    public void TurnOffPreview()
    {
        if (!towerPreview.activeInHierarchy)
        {
            return;
        }
        towerPreview.SetActive(false);
        towerPreview.transform.position = new Vector3(0,0,0);        
    }

    
}
