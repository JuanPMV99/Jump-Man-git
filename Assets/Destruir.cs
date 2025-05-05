using UnityEngine;

public class Barril : MonoBehaviour
{
    public GameObject efectoDestruccion; // arr�stralo en el Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestruirBarril"))
        {
            // Instanciar el efecto en la posici�n del barril
            Instantiate(efectoDestruccion, transform.position, Quaternion.identity);

            // Destruir el barril
            Destroy(gameObject);
        }
    }
}