using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemiesInterface;

public class MushroomIA : MonoBehaviour, IEnemy
{
    [Header("Mushroom IA")]
    [Header("Skills Settings")]
    [SerializeField] private MonoBehaviour onDieSkillPrefab;


    private EnemyController myController;
    private MushroomMovement myMovement;
    private Animator myAnimator;
    private ISkil onDieSkill;

    private List<Tile> path;

    private EnemyState myState;
   
    private bool pathCompleted = false;

    private int pathIndex = 0;

    

    //Animator hash codes
    private readonly int ANIMATOR_ATTACK_HASH = Animator.StringToHash("Attack");
    private readonly int ANIMATOR_WALK_HASH = Animator.StringToHash("Walk");
    private readonly int ANIMATOR_DIE_HASH = Animator.StringToHash("Die");
    
    private void OnEnable()
    {
        myState = EnemyState.Walking;
    }

    private void Awake()
    {
        myController = GetComponent<EnemyController>();
        myMovement = GetComponentInChildren<MushroomMovement>();
        myAnimator = GetComponentInChildren<Animator>();
        onDieSkill = Instantiate(onDieSkillPrefab, myMovement.transform.position, Quaternion.identity, myMovement.transform).GetComponent<ISkil>();
    }

    private void Update()
    {
        UpdateState();
        //Testing propose only
        if (Input.GetKeyDown(KeyCode.I))
        {
            onDieSkill?.CastSkill(transform.position);
        }
    }

    private void UpdateState()
    {
        if (!myController.IsAlive)
        {
            return;
        }

        switch (myState)
        {
            case EnemyState.Walking:
                UpdateWalking();
                break;
        }
    }

    private void UpdateWalking()
    {
        if (path==null)
        {
            return;
        }

        if (pathCompleted)
        {
            return;
        }
        myAnimator.SetBool(ANIMATOR_WALK_HASH, !myMovement.PositionReached);
        if (myMovement.PositionReached)
        {
            if (pathIndex < path.Count - 1)
            {
                pathIndex++;
                myMovement.SetPositionToMove(path[pathIndex].transform.position);
            }
            else
            {
                PathCompleted();
            }
        }
    }

    private void PathCompleted()
    {
        pathCompleted = true;
        myAnimator.SetTrigger(ANIMATOR_ATTACK_HASH);
    }

    public void AttackAnimationCompleted()
    {
        HealthMananger.Instance.TakeDamage(myController.DamageToStronghold);
        gameObject.SetActive(false);
        ResetWalkthroughPath();
    }

    private void ResetWalkthroughPath()
    {
        pathCompleted = false;
        pathIndex = 0;
        if (path==null)
        {
            return;
        }
        myMovement.transform.position = path[pathIndex].transform.position;
        myMovement.SetPositionToMove(path[pathIndex].transform.position);
    }

    public void DieAnimationCompleted()
    {
        BankMananger.Instance.AddGold(myController.GoldReward);
        gameObject.SetActive(false);
        ResetWalkthroughPath();
    }

    //Interface Implementation Methods
    public void SetPath(List<Tile> path)
    {
        this.path = path;
        ResetWalkthroughPath();
    }

    public void Die()
    {
        onDieSkill?.CastSkill(transform.position);
        myAnimator.SetTrigger(ANIMATOR_DIE_HASH);
    }

}
