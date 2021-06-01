using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sr;
    private Transform transform;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        sr.flipY = false;
        float movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        FlipPlayer(movement);

        CheckJump();
    }

    void FlipPlayer(float movement)
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

    void CheckJump()
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigidbody.velocity.y) < 0.001f)
        {
            rigidbody.AddForce(new Vector3(0, JumpForce), ForceMode2D.Impulse);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
