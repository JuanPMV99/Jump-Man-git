using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Enemigo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Colisión detectada con: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PerderVida();
        }
    }
    

  
}
