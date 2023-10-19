using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMananger : Singleton<HealthMananger>
{
    [Header("Health Mananger")]
    [SerializeField,Min(0)] private int maxHealth = 10;
    [SerializeField] private int currentHealth;
    [SerializeField] private Vector3 panelOffset = Vector3.zero;

    private UIBar healthBar;
    private bool isAlive;

    protected override void Awake()
    {
        base.Awake();
        healthBar = GetComponentInChildren<UIBar>();
    }

    private void Start()
    {
        isAlive = true;
        currentHealth = maxHealth;
        healthBar.UpdateBar(currentHealth, maxHealth);
    }
    
    private void LocatePanelInScreenSpace()
    {
        if (!Stronghold.Instance)
        {
            return;
                
        }
        transform.position = Camera.main.WorldToScreenPoint(Stronghold.Instance.transform.position) + panelOffset;
    }


    public void TakeDamage(int damage)
    {
        if (!isAlive)
        {
            return;
        }
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;
            currentHealth = 0;
            GameOverMenu.Instance.OpenGameOverMenu(GameOverMenu.GameOverMenuToOpen.GameOver);
        }

        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    public Vector3 GetStrongholdPos()
    {
        return Stronghold.Instance.transform.position;
    }
}
