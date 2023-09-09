using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour,IBuilding
{
    [Header("Tower Controller")]
    [SerializeField] private TowerData myData;

    public GameObject AmmunitionPrefab { get { return myData.AmmunitionPrefab; } }
    public int AttackDamage { get { return myData.AttackDamage; } }
    public float AttackRatio { get { return myData.AttackRatio; } }
    public float AttackRange { get { return myData.AttackRange; } }

    private void Start()
    {
        gameObject.name = myData.TowerName;
    }
    //Debugging Tools
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, myData.AttackRange);
    }
}
