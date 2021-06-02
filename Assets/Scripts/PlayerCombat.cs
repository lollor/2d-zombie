using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //public Animator animator;
    public Transform attackPoint;
    private Transform player;
    public float attackRange = 0.5f;
    private Animator animator;
    public LayerMask enemyLayers;
    public float damage = 1f;
    private float swordHitboxHeight = 0.4f;
    private PlayerMovement playerMovement;
    private Body body;


    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        player = GetComponent<Transform>();
        body = GetComponent<Body>();
    }

    // Update is called once per frame
    private bool deadAnimationDone = false;
    private float attackRate = 1f;
    private float nextAttackTime = 0f;
    void Update()
    {
        if (body.IsDead())
        {
            if (!deadAnimationDone)
            {
                animator.SetTrigger("dead");
                StartCoroutine(DelayedDeath(0.95f));
                deadAnimationDone = true;
                this.enabled = false;
                playerMovement.enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0f;
                GetComponent<Collider2D>().enabled = false;
            }
            return;
        }
        if (Time.time >= nextAttackTime)
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                animator.SetTrigger("attack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
    }
    IEnumerator DelayedDeath(float _delay = 0)
    {
        yield return new WaitForSeconds(_delay);
        GetComponent<SpriteRenderer>().enabled = false;
    }
    void Attack()
    {
        //Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        if (playerMovement.GetDirection() == -1)
        {
            attackPoint.position = new Vector2(player.position.x - attackRange * 0.4f, attackPoint.position.y);
            //Debug.LogWarning(playerMovement.GetDirection());
        }
        else if (playerMovement.GetDirection() == 1)
        {
            attackPoint.position = new Vector2(player.position.x + 0.5f, attackPoint.position.y);
        }
        Collider2D[] enemiesHit = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackRange, swordHitboxHeight), 0, enemyLayers);

        foreach (Collider2D item in enemiesHit)
        {
            if (item.name != "ground")
            {
                Debug.Log("\nHo colpito " + item.name + " attackPoint.position: " + attackPoint.position.ToString());
                item.GetComponent<EnemyAI>().WasHit(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            //Gizmos.DrawSphere(attackPoint.position, attackRange);
            //Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackRange, swordHitboxHeight));
            Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackRange, swordHitboxHeight));
        }
    }

    public void WasHit()
    {
        animator.SetTrigger("hurt");
    }
    public void WasHit(float damage)
    {
        animator.SetTrigger("hurt");
        body.RemoveLifePoints(damage);
    }
}
