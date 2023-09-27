using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeEngineAmmo : MonoBehaviour
{
    [Header("Siege Engine Ammo")]
    [SerializeField, Min(1f)] private float timeOfTravel = 1f;
    [Tooltip("Is the percentage of the distance that will translate to the max height of the curve")]
    [SerializeField, Range(0f, 100f)] private float maxHeightPercentage = 50;
    [Tooltip("Is the representaive curve off the movement")]
    [SerializeField] private AnimationCurve heightCurve;

    private float range;
    private Vector3 startPos;
    private Vector3 finalPos;
    private float maxY;

    public void SetRange(float range)
    {
        this.range = range;
        startPos = transform.position;
        finalPos = startPos + (transform.forward * range);
        maxY = (Vector3.Distance(startPos, finalPos) * maxHeightPercentage) / 100;
        StartCoroutine(ParableMovementRoutine());
    }

    private void DestroyBehaviour()
    {
        //Destroy Behaviour
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private IEnumerator ParableMovementRoutine()
    {
        float elapsedTime = 0;

        while (elapsedTime < timeOfTravel)
        {
            elapsedTime += Time.deltaTime;
            Vector3 newPos = new Vector3();
            float deltaT = elapsedTime / timeOfTravel;
            float heightT = heightCurve.Evaluate(deltaT);
            newPos.x = Mathf.Lerp(startPos.x, finalPos.x, deltaT);
            newPos.y = Mathf.Lerp(startPos.y, maxY, heightT);
            newPos.z = Mathf.Lerp(startPos.z, finalPos.z, deltaT);

            transform.position = newPos;
            yield return null;
        }

        DestroyBehaviour();
    }

}
