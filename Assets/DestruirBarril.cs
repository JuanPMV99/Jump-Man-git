using UnityEngine;

public class DestruirEnTrigger : MonoBehaviour
{
    // Se destruye si entra en cualquier trigger con el tag "DestruirBarril"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DestruirBarril"))
        {
            Destroy(gameObject);  // Destruye el barril
        }
    }
}
