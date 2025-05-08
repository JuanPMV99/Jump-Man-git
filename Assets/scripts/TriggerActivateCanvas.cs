using UnityEngine;

public class ActivarCanvasWin : MonoBehaviour
{
    public GameObject CanvasWin;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CanvasWin != null)
            {
                CanvasWin.SetActive(true);
                Debug.Log("¡CanvasWin activado!");
            }
            else
            {
                Debug.LogWarning("CanvasWin no está asignado en el Inspector.");
            }
        }
    }
}