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

    protected override void Awake()
    {
        base.Awake();
        buttons = GetComponentsInChildren<StoreManangerButton>();
    }


}
