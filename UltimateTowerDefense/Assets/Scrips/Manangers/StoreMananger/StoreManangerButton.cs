using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StoreManangerButton : MonoBehaviour,IDeselectHandler
{    
    private Button myButton;
    private TextMeshProUGUI goldCostText;
    private Image buttonIcon;
    private bool buttonSelected = false;

    public bool ButtonSelected { get { return buttonSelected; } set { buttonSelected = value; } }
    public bool ButtonPressed { get; set; }
    private void Awake()
    {
        myButton = GetComponent<Button>();
        goldCostText = GetComponentInChildren<TextMeshProUGUI>();
        buttonIcon = transform.GetChild(0).GetComponent<Image>();
        ButtonPressed = false;
    }

    private void Start()
    {
        SumitButtonEvents();
    }

    private void SumitButtonEvents()
    {
        myButton.onClick.AddListener(ButtonHasBeenSelected);       
    }

    public void SetGoldCostText(int goldCost)
    {
        goldCostText.text = goldCost.ToString();
    }

    public void SetButtonState(bool state)
    {
        myButton.interactable = state;
    }

    private void ButtonHasBeenSelected()
    {
        ButtonPressed = true;
        buttonSelected = true;
    }  

    public void OnDeselect(BaseEventData baseData)
    {
        buttonSelected = false;
    }

    public void SetButtonIcon(Sprite icon)
    {
        buttonIcon.sprite = icon;
    }

}
