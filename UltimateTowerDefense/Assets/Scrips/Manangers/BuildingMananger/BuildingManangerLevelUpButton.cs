using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingManangerLevelUpButton : MonoBehaviour
{
    private Button myButton;
    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        buttonText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SetLevelUPCost(int cost)
    {
        buttonText.text = cost.ToString();
    }

    public void SetButtonInteractable(bool state)
    {
        if (myButton.interactable != state)
        {
            myButton.interactable = state;
        }
    }
}
