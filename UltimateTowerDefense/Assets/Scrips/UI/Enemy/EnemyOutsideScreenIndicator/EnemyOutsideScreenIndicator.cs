using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;

public class EnemyOutsideScreenIndicator : Singleton<EnemyOutsideScreenIndicator>
{
    [Header("Enemy OutsideScreenIndicator")]
    [SerializeField] private Indicator indicatorPrefab;
    [SerializeField] private Vector2 indicatorPositionOffset;

    private Dictionary<EnemyController, Indicator> activeIndicators = new Dictionary<EnemyController, Indicator>();

    private void Update()
    {
        UpdateActiveIndicators();
    }

    private void UpdateActiveIndicators()
    {
        foreach (KeyValuePair<EnemyController,Indicator> keyValuePair in activeIndicators)
        {
            if (!keyValuePair.Key.gameObject.activeInHierarchy || IsInsideScreen(keyValuePair.Key.transform.position) )
            {
                if (keyValuePair.Value.gameObject.activeInHierarchy)
                {
                    keyValuePair.Value.gameObject.SetActive(false);
                }
            }
            else
            {
                if (!keyValuePair.Value.gameObject.activeInHierarchy)
                {
                    keyValuePair.Value.gameObject.SetActive(true);
                }
                UpdateIndicatorPosition(keyValuePair.Key.transform.position,keyValuePair.Value);
            }
        }
    }

    private void UpdateIndicatorPosition(Vector3 enemyPosition,Indicator indicator)
    {
        Vector3 newIndicatorPos = new Vector3();
        Vector3 enemyScreenPos = Camera.main.WorldToScreenPoint(enemyPosition);
        newIndicatorPos.x = Mathf.Clamp(enemyScreenPos.x, 0 + indicatorPositionOffset.x, Screen.width - indicatorPositionOffset.x);
        newIndicatorPos.y = Mathf.Clamp(enemyScreenPos.y, 0 + indicatorPositionOffset.y, Screen.height - indicatorPositionOffset.y);
        indicator.transform.position = newIndicatorPos;
    }

    private bool IsInsideScreen(Vector3 position)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        Vector2 screenSize = new Vector2();
        screenSize.x = Screen.width;
        screenSize.y = Screen.height;

        return (screenPosition.x >= 0 && screenPosition.x <= screenSize.x) && (screenPosition.y >= 0 && screenPosition.y <= screenSize.y);   
    }

    public void AddIndicator(EnemyController enemy)
    {
        if (activeIndicators.ContainsKey(enemy))
        {
            return;
        }
        Vector3 enemyScreenPosition = Camera.main.WorldToScreenPoint(enemy.transform.position);
        Indicator newIndicator = Instantiate(indicatorPrefab, enemyScreenPosition, Quaternion.identity, transform);
        activeIndicators.Add(enemy, newIndicator);
    }

    public void RemoveIndicator(EnemyController enemy)
    {
        if (!activeIndicators.ContainsKey(enemy))
        {
            return;
        }
        Indicator indicator = activeIndicators[enemy];
        activeIndicators.Remove(enemy);
        Destroy(indicator);
    }
}
