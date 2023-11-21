using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuncionPanelSalida : MonoBehaviour
{

    //Referencias serializadas a componentes
    [Header("Referencias a Componentes")]
    [SerializeField] public GameObject jugador;

    //Eventos disponibles para suscribirse con componentes externos
    [Header("Eventos Disponibles")]
    [SerializeField] private UnityEvent alTocarPanel;

    //Referencias a componentes
    private SpriteRenderer sprite;

    //Variables locales
    private bool fueActivado = false;

    //Iniciar: Se llama antes de la primera actualización de frame
    void Start()
    {
        //Almacena los componentes obtenidos en las variables
        sprite = GetComponent<SpriteRenderer>();

    }

    //Al entrar en trigger 2D: Se llama cuando el trigger de este objeto colisiona con otro objeto
    void OnTriggerEnter2D(Collider2D contacto) {

        //Si el objeto con el cual se colisionó está etiquetado como Jugador y el botón aun no fue activado...
        if (contacto.gameObject.tag == "Player" && !fueActivado)
        {

            Debug.Log("[INFO/MAIN]: Llegaste al Panel de Salida. Ganaste!");

            //Se desactivan los componentes requeridos y la bandera de activación cambia a verdadero
            fueActivado = true;

            alTocarPanel.Invoke();

        }

    }

}
