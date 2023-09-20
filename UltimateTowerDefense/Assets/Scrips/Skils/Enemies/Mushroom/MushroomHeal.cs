using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomHeal : MonoBehaviour,ISkil
{
    [Header("Mushroom Heal")]
    [SerializeField, Range(0, 100)] private int lifePercentageHealed = 0;
    [SerializeField, Min(0f)] private float rangeOfEffect = 0;
    private ParticleSystem visualEffect;

    private void Awake()
    {
        visualEffect = GetComponentInChildren<ParticleSystem>();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeOfEffect);
    }

    public void CastSkill(Vector3 position)
    {
        visualEffect?.Play();
        Collider[] surroundingObjects = Physics.OverlapSphere(position,rangeOfEffect);
        foreach (Collider surroundingObject in surroundingObjects)
        {
            surroundingObject.GetComponent<EnemyDamageHandler>()?.HealMaxHealthPercentage(lifePercentageHealed);
        }

    }
}
