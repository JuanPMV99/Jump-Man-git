using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float fallLimitY = -10f;

    private Rigidbody2D rb;
    private Animator animator; 
    private bool isGrounded;
    private bool facingRight = true;
    private bool canJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 
        animator.SetBool("isRunning", moveInput != 0);

        // Verificar suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded) canJump = true;

        // Saltar
        if (canJump && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            canJump = false;
        }

        // Mejora salto
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump") && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Flip
        if ((facingRight && moveInput < 0) || (!facingRight && moveInput > 0))
        {
            Flip();
        }

        // Derrota por caída
        if (transform.position.y < fallLimitY)
        {
            Derrota();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Meta"))
        {
            Victoria();
        }

        if (other.CompareTag("Peligro"))
        {
            Derrota();
        }
    }

    void Victoria()
    {
        Debug.Log("¡Victoria! 🎉");
        // SceneManager.LoadScene("SiguienteNivel");
    }

    void Derrota()
    {
        Debug.Log("Derrota 💀");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

