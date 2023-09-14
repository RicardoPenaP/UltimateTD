using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingManangerInfo : MonoBehaviour
{
    private TextMeshProUGUI buildingName;
    private TextMeshProUGUI buildingLevel;
    private BuildingManangerDescription buildingDescription;

    private void Awake()
    {
        buildingName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        buildingLevel = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        buildingDescription = GetComponentInChildren<BuildingManangerDescription>();
    }

    public void SetName(string name)
    {
        buildingName.text = name;
    }

    public void SetLevel(int level)
    {
        buildingLevel.text = $"Level: {level}";
    }


}
