using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildingManangerIcon : MonoBehaviour
{
    private Image icon;
    private void Awake()
    {
        icon = transform.GetChild(1).GetComponent<Image>();
    }

    public void SetIcon(Sprite icon)
    {
        this.icon.sprite = icon;
    }
}
