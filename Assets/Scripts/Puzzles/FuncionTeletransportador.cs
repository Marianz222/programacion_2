using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuncionTeletransportador : MonoBehaviour
{

    //Eventos disponibles para suscribir objetos
    [Header("Eventos Disponibles")]
    [SerializeField] UnityEvent enContactoConEntrada;

    //Referencias a componentes externos
    [Header("Referencias a Componentes")]
    [SerializeField] private Transform posicionSalida;

    [SerializeField] private AudioSource fuenteAudio;

    private bool fueActivado;

    void Start() {

        fuenteAudio = GetComponent<AudioSource>();

    }

    //Al Entrar en un Trigger 2D: Se activa cuando un objeto colisiona con el trigger de este objeto
    void OnTriggerEnter2D(Collider2D contacto) {

        if (contacto.gameObject.CompareTag("Player"))
        {
            dispararTeletransportacion(contacto);

        }

    }

    private void dispararTeletransportacion(Collider2D contacto) {

        GameManager.Instancia.teletransportar(contacto.gameObject, posicionSalida);

        fuenteAudio.Play();

    }
}
