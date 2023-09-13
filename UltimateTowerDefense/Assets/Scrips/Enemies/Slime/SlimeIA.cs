using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IEnenmy;

public class SlimeIA : MonoBehaviour,IEnemy
{
    private EnemyController myController;
    private SlimeMovement myMovement;

    private List<Tile> path;

    private EnemyState myState;

    private bool canWalk = true;

    public bool CanWalk { get { return canWalk; } }

    private void Awake()
    {
        myController = GetComponentInParent<EnemyController>();
        myMovement = GetComponent<SlimeMovement>();
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        switch (myState)
        {            
            case EnemyState.Walking:
                UpdateWalking();
                break;            
        }
    }

    private void UpdateWalking()
    {
        if (myMovement.PositionReached)
        {
            HealthMananger.Instance.TakeDamage(myController.DamageToStronghold);
        }
    }


    public void ResetWalkthroughPath()
    {
       
    }

    public void SetPath(List<Tile> path)
    {
        this.path = path;
    }
}
