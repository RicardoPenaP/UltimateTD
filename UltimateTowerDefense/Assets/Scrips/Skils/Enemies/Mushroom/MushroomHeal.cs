using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomHeal : MonoBehaviour,ISkil
{
    [Header("Mushroom Heal")]
    [SerializeField, Range(0, 100)] private int lifePercentageHealed = 0;

    public void CastSkill(Vector3 position)
    {

    }
}
