using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EstadoPersonaje : MonoBehaviour
{

    [Header("Asignación de Teclas")]
    [SerializeField] public KeyCode teclaEstadoA;
    [SerializeField] public KeyCode teclaEstadoB;

    [Header("Eventos Disponibles")]
    [SerializeField] public UnityEvent AlTransformarseEnCubo;
    [SerializeField] public UnityEvent AlTransformarseEnSierra;
    [SerializeField] public UnityEvent AlTransformarseEnAracnido;

    //La variable de estado controla en qué estado se encuentra el jugador, sirve para que el juego detecte qué diseño debe activar
    private int estadoActual = 0;

    //Referencias a los componentes utilizados
    private SpriteRenderer spriteCubo;
    private SpriteRenderer spriteSierra;
    private Rigidbody2D cuerpoRigidoCubo;
    private Rigidbody2D cuerpoRigidoSierra;

    void Start() {

        spriteCubo = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        spriteSierra = gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();

        cuerpoRigidoCubo = gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
        cuerpoRigidoSierra = gameObject.transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>();

        estadoActual = 0;
        //AlTransformarseEnCubo.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(teclaEstadoA) && estadoActual != 0) {

            estadoActual = 0;

            transformacionCubo();

        } else if (Input.GetKeyDown(teclaEstadoB) && estadoActual != 1) {

            estadoActual = 1;

            transformacionSierra();

        }

        if (estadoActual == 0)
        {

            //spriteCubo.enabled = true;
            //spriteSierra.enabled = false;

        } else if (estadoActual == 1) {

            //spriteCubo.enabled = false;
            //spriteSierra.enabled = true;

        }

    }

    private void transformacionCubo() {

        spriteCubo.enabled = true;
        spriteSierra.enabled = false;

        AlTransformarseEnCubo.Invoke();

    }

    private void transformacionSierra() {

        spriteCubo.enabled = false;
        spriteSierra.enabled = true;

        AlTransformarseEnCubo.Invoke();

    }

    private void transformacionAracnido() {

        //Placeholder

    }
}
