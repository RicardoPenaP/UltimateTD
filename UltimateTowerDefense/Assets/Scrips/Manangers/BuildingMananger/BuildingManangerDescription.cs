using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingManangerDescription : MonoBehaviour
{
    private TextMeshProUGUI descriptionText;
    private TextMeshProUGUI damageText;
    private TextMeshProUGUI fireRatioText;
    private TextMeshProUGUI rangeText;
    private TextMeshProUGUI ammunitionText;

    private void Awake()
    {
        descriptionText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        damageText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        fireRatioText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        rangeText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        ammunitionText = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
    }

    public void SetDescription(string description)
    {
        descriptionText.text = description;
    }

    public void SetDamage(int damage)
    {
        damageText.text = $"Damage: {damage}";
    }

    public void SetAttackRatio(float attackRatio)
    {
        fireRatioText.text = $"Fire Ratio: {attackRatio}/s";
    }

    public void SetRange(float range)
    {
        rangeText.text = $"Range: {range}";
    }

    public void SetAmmunition(string description)
    {
        ammunitionText.text = $"Ammo: {description}";
    }
}
