using UnityEngine;

public class Barril : MonoBehaviour
{
    public GameObject efectoDestruccion;
    private bool yaHizoDano = false; // bandera para evitar múltiples daños

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
            // Aquí deberías llamar a la función que le quita vida al jugador
            collision.GetComponent<PlayerMovement>()?.RecibirDano(); // suponiendo que así se llama
            yaHizoDano = true;
        }
    }
}

