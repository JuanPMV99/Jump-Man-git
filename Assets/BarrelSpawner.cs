using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    [Header("Configuración de Spawner")]
    [SerializeField] private GameObject barrilPrefab;
    [SerializeField] private float tiempoEntreSpawns = 2f;
    [SerializeField] private Vector2 impulsoInicial = new Vector2(2f, 0f);

    private float tiempoSiguienteSpawn;

    private void Update()
    {
        // Verifica si ya pasó el tiempo para instanciar un nuevo barril
        if (Time.time >= tiempoSiguienteSpawn)
        {
            SpawnBarril();
            tiempoSiguienteSpawn = Time.time + tiempoEntreSpawns;
        }
    }

    private void SpawnBarril()
    {
        // Instancia el barril en la posición del spawner
        GameObject nuevoBarril = Instantiate(barrilPrefab, transform.position, Quaternion.identity);

        // Le aplicamos fuerza para que ruede
        Rigidbody2D rb = nuevoBarril.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(impulsoInicial, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("El prefab del barril no tiene Rigidbody2D.");
        }
    }
}
