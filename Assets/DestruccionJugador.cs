using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestruccionJugador : MonoBehaviour
{

    [Header("Configuracion de Variables")]
    [SerializeField] public Vector2 puntoOrigen;
    //Este vector se usará para el respawn a futuro

    //Al ingresar a un Trigger 2D: Se llama cuando el objeto entra en contacto con un trigger externo
    private void OnTriggerEnter2D (Collider2D contacto) {

    //Si el objeto con el cual se colisionó es una fuente de daño (tiene ese tag)
    if (contacto.gameObject.CompareTag("Damage Source") && (this.transform.parent.gameObject.activeSelf == true)) {

            //Registra en consola que el juego termino y desactiva el jugador
            Debug.Log("Fuiste eliminado. Perdiste!");
            this.transform.parent.gameObject.SetActive(false);

        }

    }

}
