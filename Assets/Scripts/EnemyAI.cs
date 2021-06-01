using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float MovementSpeedEnemy = 0.5f;
    private SpriteRenderer sr;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private float time1 = 0, timeBtwMovement = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float direction = player.position.x - transform.position.x;
        Move(direction);

        if (Time.time > time1)
        {
            time1 = Time.time + timeBtwMovement;
            sr.color = Color.white;
            Attack();
        }
    }

    void Move(float movement)
    {
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeedEnemy;
        FlipEnemy(movement);
    }

    void FlipEnemy(float movement)
    {
        if (movement < 0)
        {
            sr.flipX = true;
        }
        else if (movement > 0)
        {
            sr.flipX = false;
        }
    }

    void Attack()
    {
        Collider2D[] playerHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D item in playerHit)
        {
            if (item.name != "ground")
            {
                Debug.Log("Ho colpito " + item.name);
                //item.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
