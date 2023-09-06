using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreMananger : Singleton<StoreMananger>
{
    [Header("Store Mananger")]
    [SerializeField] private List<TowerData> availableTowers;
   
    private StoreManangerButton[] buttons;

    private TowerData selectedTower;

    protected override void Awake()
    {
        base.Awake();
        buttons = new StoreManangerButton[transform.childCount];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = transform.GetChild(i).GetComponent<StoreManangerButton>();
        }
        
    }


}
