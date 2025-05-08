using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public HUD hud;
    public GameObject Canvas;
    public GameObject CanvasWin;
    public GameObject CanvasDerrota;

    private int Vidas = 3;
    private bool juegoTerminado = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PerderVida()
    {
        if (juegoTerminado) return;

        Vidas--;
        Debug.Log("Vida perdida. Vidas restantes: " + Vidas);

        if (Vidas >= 0 && Vidas < 3) // Evitar error si Vidas < 0
        {
            hud.DesactivarVida(Vidas);
        }

        if (Vidas <= 0)
        {
            Derrota();
        }
    }

    public void Meta()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;

        if (CanvasWin != null)
            CanvasWin.SetActive(true);

        Time.timeScale = 0f;
        Debug.Log("You Win");
    }

    public void Derrota()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;

        if (CanvasDerrota != null)
            CanvasDerrota.SetActive(true);

        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;

        // Reinicia los contadores lógicos
        Vidas = 3;
        juegoTerminado = false;

        StartCoroutine(RecargarNivel());
    }

    private IEnumerator RecargarNivel()
    {
        SceneManager.LoadScene("NivelPrincipal");
        yield return null; // Espera un frame para que cargue la escena

        hud = FindFirstObjectByType<HUD>();
        Canvas = GameObject.Find("Canvas"); // Si tienes un canvas general
        CanvasDerrota = GameObject.Find("CanvasDerrota"); // Reasigna el nuevo
        CanvasWin = GameObject.Find("CanvasWin"); // Reasigna el nuevo

        Debug.Log("HUD reconectado: " + hud);

        // Oculta todos los canvas
        if (CanvasDerrota != null) CanvasDerrota.SetActive(false);
        if (CanvasWin != null) CanvasWin.SetActive(false);
        if (Canvas != null) Canvas.SetActive(false);
    }


    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicial");
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}

