using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionPantallaRoja : MonoBehaviour
{

    //Referencias serializadas a componentes
    [Header("Configuracion de Tiempo")]
    [SerializeField] private int tiempoInicio = 10;
    [SerializeField] private int intervaloPulso = 2;
    [SerializeField] private int tiempoDesactivacion = 1;

    //Referencias privadas y locales a componentes
    private AudioSource sonidoAlarma;
    private SpriteRenderer pantallaRoja;
    private bool estaActiva = false;

    //Iniciar: Llamado antes de la primer actualización de frame
    void Start()
    {
        //Se obtienen todos los componentes necesarios y se almacenan en las variables antes mencionadas
        sonidoAlarma = this.GetComponent<AudioSource>();
        pantallaRoja = this.GetComponent<SpriteRenderer>();

        //Se desactiva inicialmente la pantalla roja y se llama al método de invocar repetidamente la función de activación de la alarma
        pantallaRoja.enabled = false;
        InvokeRepeating(nameof(ActivarAlarma), tiempoInicio, intervaloPulso);
    }

    //Activa la alarma (activando el sprite y reproduciendo el sonido) y llama al método de desactivación tras completarse
    private void ActivarAlarma() {

        //Si la pantalla no está activa aún... (Se ejecuta una única vez al activarse y nunca después)
        if (!estaActiva) {

            //Cambia la bandera de activación y registra por consola
            Debug.Log("La Alarma ha sido activada!");
            estaActiva = true;

        }

        //Activa el sprite y reproduce el sonido asociado
        pantallaRoja.enabled = true;
        sonidoAlarma.Play();

        //Llama al inicio de la corrutina de desactivación
        StartCoroutine(nameof(DesactivarAlarma));

    }

    //Desactiva la alarma (su sprite) [tiempoDesactivacion] segundos después de ser llamado
    IEnumerator DesactivarAlarma() {

        //Espera el tiempo necesario para continuar
        yield return new WaitForSeconds(tiempoDesactivacion);

        //Desactiva el sprite de la pantalla
        pantallaRoja.enabled = false;

    }
}
