using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ControladorCamara : MonoBehaviour
{

    //Campos serializados para configurar
    [Header("Configuracion de Teclas")]
    [SerializeField] private KeyCode teclaIncrementoZoom;
    [SerializeField] private KeyCode teclaDecrementoZoom;
    [SerializeField] private KeyCode teclaReseteoZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float minZoom;

    //Referencias privadas a variables y componentes
    private CinemachineVirtualCamera camaraPrincipal;
    private float zoomPorDefecto;
    private float zoomActual;

    //Iniciar: Se llama en la primera ejecución antes de actualizar
    void Start()
    {
        camaraPrincipal = GetComponentInChildren<CinemachineVirtualCamera>();
        zoomPorDefecto = camaraPrincipal.m_Lens.OrthographicSize;
        zoomActual = camaraPrincipal.m_Lens.OrthographicSize;
    }

    //Actualizar: Se llama constantemente cada frame
    void Update()
    {
        //Si la tecla de incrementar el zoom fue presionada...
        if (Input.GetKeyDown(teclaIncrementoZoom)) {

            //Decrementa el valor del tamaño ortográfico (acercando la cámara), además registra dicho cambio por consola
            zoomActual = zoomActual - 100 * Time.deltaTime;
            Debug.Log("[INFO/DEBUG]: Zoom incrementado en " + gameObject.name);

        //Sino si la tecla de decrementar zoom fue presionada...
        } else if (Input.GetKeyDown(teclaDecrementoZoom)) {

            //Incrementa el valor del tamaño ortográfico (alejando la cámara), además registra dicho cambio por consola
            zoomActual = zoomActual + 100 * Time.deltaTime;
            Debug.Log("[INFO/DEBUG]: Zoom decrementado en " + gameObject.name);

        //Sino si la tecla de reseteo del zoom fue presionada...
        } else if (Input.GetKeyDown(teclaReseteoZoom)) {

            //Restablece el valor original del tamaño ortográfico de cámara y registra por consola
            zoomActual = zoomPorDefecto;
            Debug.Log("[INFO/DEBUG]: Zoom reiniciado en " + gameObject.name);

        }

        //Aplica los cambios realizados en variable a el tamaño de la propia cámara
        camaraPrincipal.m_Lens.OrthographicSize = zoomActual;

    }
}
