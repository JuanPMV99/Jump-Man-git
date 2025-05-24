using UnityEngine;

public class SonidoPasos : MonoBehaviour
{
    public AudioClip[] sonidosPasos;
    public float tiempoEntrePasos = 0.4f;

    private AudioSource audioSource;
    private Rigidbody2D rb2D;
    private bool enSuelo = false;
    private float tiempoSiguientePaso = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float velocidadX = Mathf.Abs(rb2D.linearVelocity.x);

        // Solo si está en el suelo y se está moviendo horizontalmente
        if (enSuelo && velocidadX > 0.1f)
        {
            if (Time.time >= tiempoSiguientePaso)
            {
                ReproducirPaso();
                tiempoSiguientePaso = Time.time + tiempoEntrePasos;
            }
        }
    }

    void ReproducirPaso()
    {
        if (sonidosPasos.Length > 0 && !audioSource.isPlaying)
        {
            AudioClip clip = sonidosPasos[Random.Range(0, sonidosPasos.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        enSuelo = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        enSuelo = false;
    }
}
