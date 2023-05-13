using UnityEngine;

public class Monedas : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
           GameManager.ActualizarMonedas();							//Actualizamos las monedas
		    gameObject.SetActive(false);
        }
    }
}
