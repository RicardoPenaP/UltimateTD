using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Tower Controller")]
    [SerializeField] private GameObject ammunitionPrefab;
    [Tooltip("Amount of damage dealt per attack")]
    [SerializeField, Min(0)] private int attackDamage = 100;
    [Tooltip("Amount of attacks per second")]
    [SerializeField, Min(0)] private float attackRatio = 1;
    [Tooltip("The range for dectecting enemies and attack them")]
    [SerializeField, Min(0f)] private float attackRange = 10f;

    public GameObject AmmunitionPrefab { get { return ammunitionPrefab; } }
    public int AttackDamage { get { return attackDamage; } }
    public float AttackRatio { get { return attackRatio; } }
    public float AttackRange { get { return attackRange; } }

    //Debugging Tools
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
