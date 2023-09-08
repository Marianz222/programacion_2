using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EstadoPersonaje : MonoBehaviour
{

    [Header("Asignación de Teclas")]
    [SerializeField] public KeyCode teclaEstadoA;
    [SerializeField] public KeyCode teclaEstadoB;

    //La variable de estado controla en qué estado se encuentra el jugador, sirve para que el juego detecte qué diseño debe activar
    private int estadoActual = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(teclaEstadoA)) {

            estadoActual = 0;

        } else if (Input.GetKeyDown(teclaEstadoB)) {

            estadoActual = 1;
        }

        if (estadoActual == 0)
        {

            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else {

            gameObject.transform.GetChild(0).gameObject.SetActive(false);

        }

        if (estadoActual == 1)
        {

            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {

            gameObject.transform.GetChild(1).gameObject.SetActive(false);

        }

    }
}
