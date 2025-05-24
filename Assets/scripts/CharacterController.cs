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

    public bool isGrounded;
    private bool canJump = false;
    private bool facingRight = true;

    private Rigidbody2D rb;
    private Animator animator;

    public float tiempoEntreDanios = 1f;
    private float tiempoDeEnfriamiento = 0f;

    public GameObject canvasWin;

    // Sonido de pasos
    public AudioSource pasosAudioSource;

    // Sonido de salto
    public AudioSource saltoAudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (pasosAudioSource == null)
            Debug.LogWarning("No has asignado AudioSource para pasos en el Inspector.");

        if (saltoAudioSource == null)
            Debug.LogWarning("No has asignado AudioSource para salto en el Inspector.");
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded) canJump = true;

        if (canJump && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            canJump = false;

            // Reproducir sonido de salto
            if (saltoAudioSource != null)
                saltoAudioSource.Play();
        }

        // Mejora del salto (más natural)
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump") && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if ((facingRight && moveInput < 0) || (!facingRight && moveInput > 0))
        {
            Flip();
        }

        // Animaciones
        animator.SetBool("isRunning", Mathf.Abs(moveInput) > 0);

        // Sonido de pasos
        if (isGrounded && Mathf.Abs(moveInput) > 0)
        {
            if (!pasosAudioSource.isPlaying)
            {
                pasosAudioSource.Play();
            }
        }
        else
        {
            if (pasosAudioSource.isPlaying)
            {
                pasosAudioSource.Stop();
            }
        }

        if (transform.position.y < fallLimitY)
        {
            Derrota();
        }

        if (tiempoDeEnfriamiento > 0f)
        {
            tiempoDeEnfriamiento -= Time.deltaTime;
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

        if (other.CompareTag("Peligro") && tiempoDeEnfriamiento <= 0f)
        {
            RecibirDano();
            tiempoDeEnfriamiento = tiempoEntreDanios;
        }
    }

    void Victoria()
    {
        Debug.Log("¡Victoria! 🎉");
        if (canvasWin != null)
        {
            canvasWin.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void Derrota()
    {
        Debug.Log("Derrota 💀");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RecibirDano()
    {
        Debug.Log("¡Recibiste daño!");
        Derrota();
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
