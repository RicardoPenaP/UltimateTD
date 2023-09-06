using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManangerButton : MonoBehaviour
{
    
    private Button myButton;
    private TextMeshProUGUI buttonText;

    private bool buttonPressed = false;

    public bool ButtonPressed { get { return buttonPressed; } }
    private void Awake()
    {
        myButton = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

    }

    private void Start()
    {
        SumitButtonEvents();
    }

    private void Update()
    {
        buttonPressed = false;
    }

    private void SumitButtonEvents()
    {
        myButton.onClick.AddListener(ButtonHasBeenPressed);        
    }

    public void SetButtonText(string newText)
    {
        buttonText.text = newText;
    }

    public void SetButtonState(bool state)
    {
        myButton.interactable = state;
    }

    private void ButtonHasBeenPressed()
    {
        buttonPressed = true;
    }

}
