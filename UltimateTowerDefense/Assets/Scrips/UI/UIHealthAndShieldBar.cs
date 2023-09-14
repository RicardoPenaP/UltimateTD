using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthAndShieldBar : MonoBehaviour
{    
    private TextMeshProUGUI nameText;
    private Image healthFill;
    private Image shieldFill;

    private void Awake()
    {
        nameText = GetComponentInChildren<TextMeshProUGUI>();
        healthFill = transform.GetChild(1).GetComponent<Image>();
        shieldFill = transform.GetChild(2).GetComponent<Image>();
    }

    public void UpdateBar(int healthCurrentValue, int healthMaxValue, int shieldCurrentValue, int shieldMaxValue)
    {   
        healthFill.fillAmount = (float)healthCurrentValue / (float)healthMaxValue;
        shieldFill.fillAmount = (float)shieldCurrentValue / (float)shieldMaxValue;
    }

    public void UpdateBar(float currentValue, float maxValue)
    {
        nameText.text = $"{currentValue}/{maxValue}";
        healthFill.fillAmount = currentValue / maxValue;
    }
}
