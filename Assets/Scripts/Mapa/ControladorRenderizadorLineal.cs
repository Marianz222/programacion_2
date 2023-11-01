using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderingController : MonoBehaviour
{

    //Campos de variable serializados
    [Header("Configuracion de Posiciones")]
    [SerializeField] public Transform posicionInicial;
    [SerializeField] public Transform posicionFinal;

    //Referencias a componentes y variables
    private LineRenderer linea;
    private bool debeRenderizarse = true;
    private int intervaloBorrado = 1;

    //Iniciar: Se llama antes de la primera actualización de frame
    void Start()
    {
        //Obtiene el componente de Renderizador de Linea, le asigna su cantidad de puntos, además de las coordenadas de dichos puntos
        linea = GetComponent<LineRenderer>();
        linea.positionCount = 2;
        linea.SetPosition(0, posicionInicial.position);
        linea.SetPosition(1, posicionFinal.position);

        Debug.DrawLine(posicionInicial.position, posicionFinal.position, Color.cyan, 25.0f);
        
    }

    //Actualización: Se llama en cada frame
    void Update() {
        
        //Si debe renderizarse...
        if (debeRenderizarse)
        {
            //Inicia la corrutina para borrar la linea y cambia la variable de debe renderizarse a falso
            StartCoroutine(nameof(LimpiarLinea));
            debeRenderizarse = false;
        }
    }

    //Desactiva el componente de Renderizador de linea, esto después de pasar [intervaloBorrado] segundos
    IEnumerator LimpiarLinea() {

        //Espera la cantidad de segundos requeridos para reanudar la corrutina
        yield return new WaitForSeconds(intervaloBorrado);

        //Desactiva el componente de linea
        linea.enabled = false;

    }
}
