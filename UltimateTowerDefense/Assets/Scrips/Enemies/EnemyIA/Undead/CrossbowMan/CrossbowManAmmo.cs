using UnityEngine;

public class CrossbowManAmmo : MonoBehaviour
{
    [Header("Crossbow Man Ammo")]
    [SerializeField,Min(0f)] private float movementSpeed;

    private float range;
    private Vector3 staringPos;

    private void Awake()
    {
        staringPos = transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.fixedDeltaTime;
        if (Vector3.Distance(transform.position,staringPos)>= range)
        {
            SelfDestroy();
        }
    }

    private void SelfDestroy()
    {
        gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    public void SetRange(float range)
    {
        this.range = range;
    }

}
