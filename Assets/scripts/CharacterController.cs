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

    public float tiempoEntreDanios = 1f; // Tiempo de espera entre un daño y otro
    private float tiempoDeEnfriamiento = 0f; // Tiempo que tiene que esperar antes de recibir otro daño
    
    public GameObject canvasWin;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento horizontal usando linearVelocity
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Verificar si está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded) canJump = true;

        // Lógica para saltar
        if (canJump && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            canJump = false;
        }

        // Control de caída usando linearVelocity
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump") && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Flip de dirección
        if ((facingRight && moveInput < 0) || (!facingRight && moveInput > 0))
        {
            Flip();
        }

        // Animación de correr
        animator.SetBool("isRunning", moveInput != 0);

        // Verificar caída fuera del límite
        if (transform.position.y < fallLimitY)
        {
            Derrota();
        }

        // Reducir el tiempo de enfriamiento
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

    // Función de colisión con objetos peligrosos (como el barril)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Meta"))
        {
            Victoria();
        }

        // Solo aplicar daño si el jugador no está en enfriamiento
        if (other.CompareTag("Peligro") && tiempoDeEnfriamiento <= 0f)
        {
            // Llamar a la función de derrota
            Derrota();

            // Establecer el tiempo de enfriamiento para evitar recibir daño repetido
            tiempoDeEnfriamiento = tiempoEntreDanios;
        }
    }

    // Función de victoria
    void Victoria()
    {
        Debug.Log("¡Victoria! 🎉");
        if (canvasWin != null)
        {
            canvasWin.SetActive(true);
            Time.timeScale = 0f; // opcional: pausa el juego al ganar
        }
    }

    // Función de derrota
    void Derrota()
    {
        Debug.Log("Derrota 💀");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reiniciar la escena actual
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
    public void RecibirDano()
    {
        if (Time.time >= tiempoDeEnfriamiento)
        {
            tiempoDeEnfriamiento = Time.time + tiempoEntreDanios;
            // Aquí quitas vida, puedes mostrar animación o sonido, etc.
            Debug.Log("¡Recibiste daño!");
            // Aquí decides si recargar escena, disminuir vida, etc.
        }
    }

}
