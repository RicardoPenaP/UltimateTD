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

    [Header("Rewards Settings")]
    [SerializeField,Min(0f)] private int goldReward = 25;

    private EnemyMovement myMovement;

    private int currentHealth;
    
    public float MovementSpeed { get { return movementSpeed; } }
    public bool CanMove { get { return canMove; } }
    public float DistanceFromNextTileOffset { get { return distanceFromNextTileOffset; } }

    private void Awake()
    {
        myMovement = GetComponent<EnemyMovement>();
    }

    private void OnEnable()
    {
        ResetEnemy();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Die()
    {
        //Implement die behaviour
        BankMananger.Instance.AddGold(goldReward);
        gameObject.SetActive(false);
        
    }

    private void ResetEnemy()
    {
        transform.localPosition = Vector3.zero;
        currentHealth = maxHealth;
        canMove = true;
        myMovement.ResetWalkthroughPath();
    }

    public void SetEnemyPath(List<Tile> newPath)
    {
        myMovement.SetPath(newPath);
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
