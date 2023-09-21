using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomHeal : MonoBehaviour,ISkil
{
    [Header("Mushroom Heal")]
    [SerializeField, Range(0, 100)] private int lifePercentageHealedPerSecond = 0;
    [SerializeField, Min(0f)] private float rangeOfTheEffect = 0f;
    [SerializeField, Min(0f)] private float durationOfTheEffect = 0f;


    private ParticleSystem visualEffect;

    private void Awake()
    {
        visualEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeOfTheEffect);
    }

    public void CastSkill()
    {
        visualEffect.transform.localScale = new Vector3(rangeOfTheEffect * 2, 1, rangeOfTheEffect * 2);
        StartCoroutine(SkillActiveRoutine());
    }

    private IEnumerator SkillActiveRoutine()
    {
        int timer = 0;

        while (timer < durationOfTheEffect)
        {
            if (!visualEffect.isPlaying)
            {
                visualEffect?.Play();
            }
            Collider[] surroundingObjects = Physics.OverlapSphere(transform.position, rangeOfTheEffect);
            foreach (Collider surroundingObject in surroundingObjects)
            {
                //surroundingObject.GetComponent<EnemyDamageHandler>()?.HealMaxHealthPercentage(lifePercentageHealedPerSecond);
            }
            yield return new WaitForSeconds(1f);
            timer++;
        }

        if (visualEffect.isPlaying)
        {
            visualEffect?.Stop();
        }
    }
}
