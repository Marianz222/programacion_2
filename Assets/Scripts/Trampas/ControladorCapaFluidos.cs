using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControladorCapaFluidos : MonoBehaviour
{

    [SerializeField] private UnityEvent alColisionarConFluido;

    void OnCollisionEnter2D(Collision2D contacto)
    {

        if (contacto.gameObject.CompareTag("Player"))
        {

            alColisionarConFluido.Invoke();

        }

    }

    void OnTriggerEnter2D(Collider2D contacto)
    {

        if (contacto.gameObject.CompareTag("Player"))
        {

            alColisionarConFluido.Invoke();

        }

    }

}
