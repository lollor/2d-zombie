using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float MovementSpeedEnemy = 1f;
    private SpriteRenderer sr;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private float time1 = 0, timeBtwAttack = 1.5f;
    private Body body;
    public float damage = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        body = GetComponent<Body>();
    }
    private bool isIdle = false;
    private bool deadAnimationDone = false;
    private bool playerDead = false;
    private bool zombieMustDoSomething = true;
    void Update()
    {
        if (playerDead)
        {
            if (zombieMustDoSomething)
            {
                animator.SetBool("isIdle", true);
                zombieMustDoSomething = false;
            }
            return;
        }
        if (body.IsDead())
        {
            if (!deadAnimationDone)
            {
                animator.SetTrigger("dead");
                deadAnimationDone = true;
                GetComponent<Body>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
            }
            return;
        }
        float direction;
        if (player.position.x - transform.position.x < 0)
        {
            direction = -1f;
        }
        else
        {
            direction = 1f;
        }
        if (!isIdle)
            Move(direction);

        if (Time.time > time1)
        {
            time1 = Time.time + timeBtwAttack;
            sr.color = Color.white;
            isIdle = false;
            Attack();
        }
    }

    void Move(float movement)
    {
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeedEnemy;
        animator.SetBool("isIdle", false);
        if (transform.rotation.z != 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
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
        
        bool haAttaccato = false;
        foreach (Collider2D item in playerHit)
        {
            if (item.name != "ground")
            {
                Debug.Log("Ho colpito " + item.name);
                //item.GetComponent<SpriteRenderer>().color = Color.red;
                item.GetComponent<PlayerCombat>().WasHit(damage);
                playerDead = item.GetComponent<Body>().IsDead();
                if (!haAttaccato)
                {
                    isIdle = true;
                    animator.SetTrigger("attack");
                    haAttaccato = true;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawSphere(attackPoint.position, attackRange);
    }




    public void WasHit()
    {
        isIdle = true;
        animator.SetTrigger("hurt");
    }
    public void WasHit(float damage)
    {
        isIdle = true;
        animator.SetTrigger("hurt");
        body.RemoveLifePoints(damage);
    }
}
