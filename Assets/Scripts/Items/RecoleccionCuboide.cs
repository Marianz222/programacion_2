using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecoleccionCuboide : MonoBehaviour
{

    [Header("Eventos Disponibles")]
    [SerializeField] private UnityEvent alRecolectarCuboide;

    private BoxCollider2D colisionador;

    void Start() {

        colisionador = GetComponent<BoxCollider2D>();

    }

    void OnTriggerEnter2D(Collider2D contacto) {

        if (!contacto.gameObject.CompareTag("Player")) { return; }

        gameObject.SetActive(false);

        alRecolectarCuboide.Invoke();

    }

}
