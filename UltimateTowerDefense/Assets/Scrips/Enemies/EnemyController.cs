using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEnenmy;

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
    private IEnemy myEnemy;

    private int currentHealth;
    
    public float MovementSpeed { get { return movementSpeed; } }
    public bool CanMove { get { return canMove; } }
    public float DistanceFromNextTileOffset { get { return distanceFromNextTileOffset; } }
    public int DamageToStronghold { get { return damageToStronghold; } }

    private void Awake()
    {        
        myEnemy = GetComponentInChildren<IEnemy>();
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

    private void Die()
    {
        BankMananger.Instance.AddGold(goldReward);
        gameObject.SetActive(false);
    }

    private void ResetEnemy()
    {
        currentHealth = maxHealth;
        canMove = true;       
    }

    public void SetEnemyPath(List<Tile> newPath)
    {
        myEnemy.SetPath(newPath);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }


   
}
