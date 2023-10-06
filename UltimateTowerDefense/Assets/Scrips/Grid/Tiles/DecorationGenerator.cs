using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationGenerator : MonoBehaviour
{
    [Header("Decoration Generator")]
    [SerializeField] private GameObject[] decorationPrefabs;

    private Tile myTile;    

    private void Awake()
    {
        myTile = GetComponentInParent<Tile>();
    }

    private void Start()
    {
        if (myTile.hasDecoration)
        {           
            InstantiateDecoration();
        }
    }

    private void InstantiateDecoration()
    {
        foreach (Transform decorationPoint in transform)
        {
            int itemToInstantiate = Random.Range(0, decorationPrefabs.Length);
            Instantiate(decorationPrefabs[itemToInstantiate], decorationPoint.transform.position, Random.rotation, decorationPoint);
        }
    }

}
