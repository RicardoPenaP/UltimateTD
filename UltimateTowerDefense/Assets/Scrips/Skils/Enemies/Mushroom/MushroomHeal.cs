using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomHeal : MonoBehaviour,ISkil
{
    [Header("Mushroom Heal")]
    [SerializeField, Range(0, 100)] private int lifePercentageHealedPerSecond = 0;
    [SerializeField, Min(0f)] private float rangeOfEffect = 0f;
    [SerializeField, Min(0f)] private float durationOfEffect = 0f;

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
        visualEffect.transform.localScale = new Vector3(rangeOfEffect*2,1, rangeOfEffect * 2);
        visualEffect?.Play();
        Collider[] surroundingObjects = Physics.OverlapSphere(position,rangeOfEffect);
        foreach (Collider surroundingObject in surroundingObjects)
        {
            surroundingObject.GetComponent<EnemyDamageHandler>()?.HealMaxHealthPercentage(lifePercentageHealedPerSecond);
        }

    }
}
