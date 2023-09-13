using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Controller")]

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool canMove;
    [SerializeField] private float distanceFromNextTileOffset = 0.05f;

    [Header("Rewards Settings")]
    [SerializeField,Min(0f)] private int goldReward = 25;

    [Header("Damage Settings")]
    [SerializeField] private int damageToStronghold = 1;

    //private EnemyMovement myMovement;
    private IEnemy myEnemyIA;

    [SerializeField]private int currentHealth;

    private bool isAlive = true;
    
    public float MovementSpeed { get { return movementSpeed; } }
    public bool CanMove { get { return canMove; } }
    public float DistanceFromNextTileOffset { get { return distanceFromNextTileOffset; } }
    public int GoldReward { get { return goldReward; } }
    public int DamageToStronghold { get { return damageToStronghold; } }
    public bool IsAlive { get { return isAlive; } }

    private void Awake()
    {        
        myEnemyIA = GetComponentInChildren<IEnemy>();
    }

    private void OnEnable()
    {
        ResetEnemy();
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero; 
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void ResetEnemy()
    {
        currentHealth = maxHealth;
        canMove = true;
        isAlive = true;
    }

    public void SetEnemyPath(List<Tile> newPath)
    {
        myEnemyIA.SetPath(newPath);
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isAlive)
        {
            return;
        }
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            isAlive = false;
            currentHealth = 0;
            myEnemyIA.Die();
        }
    }


   
}
