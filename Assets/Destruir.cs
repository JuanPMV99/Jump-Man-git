using UnityEngine;

public class Barril : MonoBehaviour
{
    public GameObject efectoDestruccion;
    private bool yaHizoDano = false; // bandera para evitar m�ltiples da�os

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestruirBarril"))
        {
            if (efectoDestruccion != null)
            {
                Instantiate(efectoDestruccion, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player") && !yaHizoDano)
        {
            // Aqu� deber�as llamar a la funci�n que le quita vida al jugador
            collision.GetComponent<PlayerMovement>()?.RecibirDano(); // suponiendo que as� se llama
            yaHizoDano = true;
        }
    }
}

