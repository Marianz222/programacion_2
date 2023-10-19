using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControladorIconoJugador : MonoBehaviour
{

    [SerializeField] private UnityEvent eventoActualizarAleatoriamente;
    [SerializeField] private int tiempoMaximo = 6;

    [SerializeField] private int tiempoMinimo = 4;

    void Start() {

        StartCoroutine(llamarEvento(obtenerTiempoAleatorio()));

    }

    private int obtenerTiempoAleatorio() {

        int numeroAleatorio = Random.Range(tiempoMinimo, tiempoMaximo);

        return numeroAleatorio;

    }

    IEnumerator llamarEvento(int tiempo) {

        yield return new WaitForSeconds(tiempo);

        eventoActualizarAleatoriamente.Invoke();

        llamarEvento(tiempo);

    }

}
