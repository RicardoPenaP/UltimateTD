using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBar : MonoBehaviour
{
    [Header("UIBar")]
    private TextMeshProUGUI barText;
    private Image fillImage;

    private void Awake()
    {
        barText = GetComponentInChildren<TextMeshProUGUI>();
        fillImage = transform.GetChild(1).GetComponent<Image>();
    }

    public void UpdateBar(int currentValue, int maxValue)
    {
        barText.text = $"{currentValue}/{maxValue}";
        fillImage.fillAmount = (float)currentValue / (float)maxValue;
    }

    public void UpdateBar(float currentValue, float maxValue)
    {
        barText.text = $"{currentValue}/{maxValue}";
        fillImage.fillAmount = currentValue / maxValue;
    }
}
