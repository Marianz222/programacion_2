using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionBotonFinal : MonoBehaviour
{

    //Referencias serializadas a componentes
    [Header("Referencias a Componentes")]
    [SerializeField] public Sprite spriteActivacion;
    [SerializeField] public GameObject jugador;

    //Referencias a componentes
    private SpriteRenderer sprite;
    private MovimientoPersonaje scriptMovimiento;
    private SaltoPersonaje scriptSalto;

    //Variables locales
    private bool fueActivado = false;

    //Iniciar: Se llama antes de la primera actualización de frame
    void Start()
    {
        //Almacena los componentes obtenidos en las variables
        sprite = GetComponent<SpriteRenderer>();
        scriptMovimiento = jugador.gameObject.GetComponentInChildren<MovimientoPersonaje>();
        scriptSalto = jugador.gameObject.GetComponentInChildren<SaltoPersonaje>();

    }

    //Al entrar en trigger 2D: Se llama cuando el trigger de este objeto colisiona con otro objeto
    void OnTriggerEnter2D(Collider2D contacto) {

        //Si el objeto con el cual se colisionó está etiquetado como Jugador y el botón aun no fue activado...
        if (contacto.gameObject.tag == "Player" && !fueActivado)
        {

            //Intercambia el sprite original con el del estado activado y registra en consola que se terminó el juego
            sprite.sprite = spriteActivacion;
            Debug.Log("Llegaste al Boton. Ganaste!");

            //Se desactivan los componentes requeridos y la bandera de activación cambia a verdadero
            scriptMovimiento.enabled = false;
            scriptSalto.enabled = false;
            fueActivado = true;
        }

    }
}
