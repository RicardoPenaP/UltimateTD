using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Controller")]

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool canMove;
    [SerializeField] private float distanceFromNextTileOffset = 0.05f;

    private EnemyMovement myMovement;

    private int currentHealth;
    private List<Tile> path;

    public float MovementSpeed { get { return movementSpeed; } }
    public bool CanMove { get { return canMove; } }
    public float DistanceFromNextTileOffset { get { return distanceFromNextTileOffset; } }
    private void Awake()
    {
        myMovement = GetComponent<EnemyMovement>();
    }
}
