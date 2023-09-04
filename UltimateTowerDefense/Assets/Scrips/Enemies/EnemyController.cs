using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Controller")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int movementSpeed;
    [SerializeField] private bool canMove;

    private EnemyMovement myMovement;

    private int currentHealth;
    private List<Tile> path;

    
    private void Awake()
    {
        myMovement = GetComponent<EnemyMovement>();
    }
}
