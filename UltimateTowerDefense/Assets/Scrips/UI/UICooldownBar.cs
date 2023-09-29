using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICooldownBar : MonoBehaviour
{
    private Image fillImage;
    private void Awake()
    {
        fillImage = GetComponentInChildren<Image>();
    }
    public void UpdateFillImage(float currentAmount, float maxAmount)
    {
        fillImage.fillAmount = currentAmount / maxAmount;
    }
}
