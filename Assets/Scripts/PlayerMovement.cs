using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sr;
    private Transform transform;
    private Animator animator;
    private int direction;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    float time1 = 0, timeBtwSome = 0.5f;
    void Update()
    {
        if (Time.time > time1)
        {
            //sr.color = Color.white;
            time1 = Time.time + timeBtwSome;
        }
        sr.flipY = false;
        float movement = Input.GetAxis("Horizontal");
        if (movement != 0)
        {
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isIdle", true);
        }

        FlipPlayer(movement);

        CheckJump();

        if (rigidbody.velocity.y == 0)
        {
            animator.SetBool("isJumping", false);
        }
    }

    void FlipPlayer(float movement)
    {
        if (movement < 0)
        {
            sr.flipX = true;
            direction = -1;
        }
        else if (movement > 0)
        {
            sr.flipX = false;
            direction = 1;
        }
    }

    void CheckJump()
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigidbody.velocity.y) < 0.001f)
        {
            if (transform.eulerAngles != new Vector3(0,0,0))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                return;
            }
            rigidbody.AddForce(new Vector3(0, JumpForce), ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
    }
    public int GetDirection()
    {
        return direction;
    }
}
