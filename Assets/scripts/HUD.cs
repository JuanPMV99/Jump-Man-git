using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject[] vidas; 

    public void DesactivarVida (int indice)
    {
        vidas[indice].SetActive(false);
    }
    public void ActivarVida(int indice)
    {
        vidas[indice].SetActive(true); 
    }
 
   
}

