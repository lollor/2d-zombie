using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float MovementSpeed = 1f;
    private SpriteRenderer sr;
    private Transform transform;
    private float time1 = 0, timeBtwMovement = 0.5f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        float movement = 1;

        if (Time.time > time1)
        {
            time1 = Time.time + timeBtwMovement;
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
        }

    }
}
