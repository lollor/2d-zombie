using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    private Rigidbody2D rigidbody;
    private SpriteRenderer[] sr;
    private Transform transform;
    public Animator animator;
    private int direction;

    void Start()
    {
        sr = new SpriteRenderer[3];
        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        sr[0] = transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
        sr[1] = transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>();
        sr[2] = transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>();
        //sr = 

        //animator = transform.GetChild(1).GetComponent<Animator>();
    }

    float time1 = 0, timeBtwSome = 0.5f;
    void Update()
    {
        if (Time.time > time1)
        {
            //sr.color = Color.white;
            time1 = Time.time + timeBtwSome;
        }
        FlipPlayerY();  
        //float movement = Input.GetAxis("Horizontal");
        float movement = 0;
        if (movement != 0)
        {
            if (transform.eulerAngles != new Vector3(0, 0, 0))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isIdle", true);
        }

        FlipPlayerX(movement);

        CheckJump();

        if (rigidbody.velocity.y == 0)
        {
            animator.SetBool("isJumping", false);
        }
    }
    void FlipPlayerY()
    {
        foreach (SpriteRenderer item in sr)
        {
            item.flipY = false;
        }
    }
    void FlipPlayerX(float movement)
    {
        if (movement < 0)
        {
            foreach (SpriteRenderer item in sr)
            {
                item.flipX = true;
            }
            direction = -1;
        }
        else if (movement > 0)
        {
            foreach (SpriteRenderer item in sr)
            {
                item.flipX = false;
            }
            direction = 1;
        }
    }

    void CheckJump()
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigidbody.velocity.y) < 0.001f)
        {
            rigidbody.AddForce(new Vector3(0, JumpForce), ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
    }
    public int GetDirection()
    {
        return direction;
    }
}
