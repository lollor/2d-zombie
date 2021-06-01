using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    private Animator animator;
    public LayerMask enemyLayers;

    private float swordHitboxHeight = 0.4f;
    private PlayerMovement playerMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
            animator.SetTrigger("attack");
        }
    }

    void Attack()
    {
        //Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        float angle = 90;
        if (playerMovement.GetDirection() == -1)
            angle = 270;
        Collider2D[] enemiesHit = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackRange, swordHitboxHeight), 270, enemyLayers);

        foreach (Collider2D item in enemiesHit)
        {
            if (item.name != "ground")
            {
                Debug.Log("Ho colpito " + item.name);
                item.GetComponent<SpriteRenderer>().color = Color.red;
                
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            //Gizmos.DrawSphere(attackPoint.position, attackRange);
            Gizmos.DrawCube(attackPoint.position, new Vector3(attackRange, swordHitboxHeight));
        }
    }
}
