using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingManangerSellButton : MonoBehaviour
{
    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        buttonText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SetSellCost(int cost)
    {
        buttonText.text = cost.ToString();
    }
}
