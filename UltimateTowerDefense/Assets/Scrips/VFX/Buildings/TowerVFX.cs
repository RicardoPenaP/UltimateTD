using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerVFXToPlay { Creation, LevelUP, Selling}
public class TowerVFX : MonoBehaviour
{
    private void Awake()
    {
        ParticleSystem levelUpVFX = GetComponentInChildren<ParticleSystem>();
        TowerController myTowerController = GetComponent<TowerController>();
    }
}
