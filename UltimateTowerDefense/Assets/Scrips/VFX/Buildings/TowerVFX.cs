using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerVFXToPlay { Creation, LevelUP, Selling}
public class TowerVFX : MonoBehaviour
{
    TowerController myTowerController;
    private ParticleSystem levelUpVFX;

    private void Awake()
    {
        levelUpVFX = GetComponentInChildren<ParticleSystem>();
        myTowerController = GetComponentInParent<TowerController>();
        myTowerController.OnLevelUp += levelUpVFX.Play;
    }

    private void OnDestroy()
    {
        if (!myTowerController)
        {
            return;
        }
        myTowerController.OnLevelUp -= levelUpVFX.Play;
    }


}
