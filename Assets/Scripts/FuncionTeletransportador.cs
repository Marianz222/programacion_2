using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionTeletransportador : MonoBehaviour
{

    //Referencias serializadas a componentes externos
    [Header("Referencias a Componentes")]
    [SerializeField] private GameObject teleportEntrada;
    [SerializeField] private GameObject teleportSalida;

    //WIP
    void OnTriggerEnter2D(Collider2D contacto) {

    if (contacto.gameObject.tag == "Player") {

            Debug.Log("El jugador lo toco");

        }

    }
}
