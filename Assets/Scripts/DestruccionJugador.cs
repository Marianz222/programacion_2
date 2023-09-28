using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestruccionJugador : MonoBehaviour
{

    //Campos serializados para componentes
    [Header("Configuracion de Variables")]
    [SerializeField] public Vector2 puntoOrigen;

    //Al ingresar a un Trigger 2D: Se llama cuando el objeto entra en contacto con un trigger externo
    private void OnTriggerEnter2D(Collider2D contacto)
    {

        //Llama al procedimiento suministrando el Collider2D como parámetro (actúa primera versión)
        procedimientoMuerte(contacto);

    }

    //Al colisionar en 2D: Se llama cuando el objeto entra en contacto con un colisionador externo
    private void OnCollisionEnter2D(Collision2D contacto) {

        //Llama al procedimiento suministrando la Collision2D como parámetro (actúa su sobrecarga)
        procedimientoMuerte(contacto);

    }

    //Si se detecta una colision entre el trigger de [contacto] y el jugador, procede destruyendo al mismo.
    private void procedimientoMuerte(Collider2D contacto) {

        //Si el objeto con el cual se colisionó es una fuente de daño (tiene ese tag)
        if (contacto.gameObject.CompareTag("Damage Source") && (this.transform.parent.gameObject.activeSelf == true))
        {

            //Registra en consola que el juego termino y desactiva el jugador
            Debug.Log("Fuiste eliminado. Perdiste!");
            this.transform.parent.gameObject.SetActive(false);

        }

    }

    //Si se detecta una colision entre [contacto] y jugador, procede destruyendo al mismo. Sobrecarga del método original
    private void procedimientoMuerte(Collision2D contacto) {

        //Si el objeto con el cual se colisionó es una fuente de daño (tiene ese tag)
        if (contacto.gameObject.CompareTag("Damage Source") && (this.transform.parent.gameObject.activeSelf == true))
        {

            //Registra en consola que el juego termino y desactiva el jugador
            Debug.Log("Fuiste eliminado. Perdiste!");
            this.transform.parent.gameObject.SetActive(false);

        }

    }

}
