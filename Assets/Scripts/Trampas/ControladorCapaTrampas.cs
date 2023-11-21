using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControladorCapaTrampas : MonoBehaviour
{

    [SerializeField] private UnityEvent alColisionarConTrampa;

    void OnCollisionEnter2D(Collision2D contacto) {

        if (contacto.gameObject.CompareTag("Player")) {

            alColisionarConTrampa.Invoke();

        }

    }

    void OnTriggerEnter2D(Collider2D contacto)
    {

        if (contacto.gameObject.CompareTag("Player"))
        {

            alColisionarConTrampa.Invoke();

        }

    }

}
