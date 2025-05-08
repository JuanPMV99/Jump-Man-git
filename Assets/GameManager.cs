using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public HUD hud;
    public GameObject Canvas;
    public GameObject CanvasWin;

    private int vidas = 3;
    private bool juegoTerminado = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void QuitarVida()
    {
        if (juegoTerminado || vidas <= 0) return;

        vidas--;

        if (hud != null && vidas >= 0 && vidas < hud.vidas.Length)
        {
            hud.DesactivarVida(vidas);
        }

        if (vidas <= 0)
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

        if (Canvas != null)
            Canvas.SetActive(true);

        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }
    

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("NivelPrincipal");
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
