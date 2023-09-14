using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingInterface;

public class TowerController : MonoBehaviour,IBuilding
{
    [Header("Tower Controller")]
    [SerializeField] private TowerData myData;

    private int currentLevel;
    private int currentAttackDamage;
    private float currentAttackRatio;
    private float currentAttackRange;

    public GameObject AmmunitionPrefab { get { return myData.AmmunitionPrefab; } }
    public int AttackDamage { get { return myData.BaseAttackDamage; } }
    public float AttackRatio { get { return myData.BaseAttackRatio; } }
    public float AttackRange { get { return myData.BaseAttackRange; } }

    private void Start()
    {
        SetTowerValues();
        
    }
    //Debugging Tools
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, myData.BaseAttackRange);
    }

    private void SetTowerValues()
    {
        gameObject.name = myData.TowerName;
        currentLevel = 1;
        currentAttackDamage = myData.BaseAttackDamage;
        currentAttackRatio = myData.BaseAttackRatio;
    }
}
