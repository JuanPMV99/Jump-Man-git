using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public float climbSpeed = 3f;
    private bool isClimbing = false;
    private float vertical;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");

        if (isClimbing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * climbSpeed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            isClimbing = false;
        }
    }
}

