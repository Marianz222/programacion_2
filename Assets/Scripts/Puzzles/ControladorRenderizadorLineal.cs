using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderingController : MonoBehaviour
{

    [Header("Variables a Configurar")]
    [SerializeField] private Gradient color;
    [SerializeField] private float grosor;

    //Campos de variable serializados
    [Header("Referencias a Componentes")]
    [SerializeField] private Transform[] posicionesLinea;

    //Referencias a componentes y variables
    private LineRenderer linea;

    //Variables locales y privadas
    private float intervaloBorrado = 0.2f;

    //Iniciar: Se llama antes de la primera actualización de frame
    void Start()
    {
        //Obtiene el componente de Renderizador de Linea, le asigna su cantidad de puntos, además de las coordenadas de dichos puntos
        linea = GetComponent<LineRenderer>();

        inicializarLinea();

    }

    private void inicializarLinea() {

        linea.enabled = false;

        linea.colorGradient = color;
        linea.startWidth = grosor;
        linea.positionCount = posicionesLinea.Length;

        for (int i = 0; i < posicionesLinea.Length; i++)
        {

            linea.SetPosition(i, posicionesLinea[i].position);

        }
    }

    public void activarLinea() {

        linea.enabled = true;

        StartCoroutine(nameof(LimpiarLinea));

    }

    //Desactiva el componente de Renderizador de linea, esto después de pasar [intervaloBorrado] segundos
    IEnumerator LimpiarLinea() {

        //Espera la cantidad de segundos requeridos para reanudar la corrutina
        yield return new WaitForSeconds(intervaloBorrado);

        //Desactiva el componente de linea
        linea.enabled = false;

    }
}
