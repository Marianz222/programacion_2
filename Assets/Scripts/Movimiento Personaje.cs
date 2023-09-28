using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Mover : MonoBehaviour
{
    //Se crea el título de la sección de campos serializados, además de añadir los mismos
    [Header("Configuracion de Variables")]
    [SerializeField] float velocidad = 5f;

    //Variables privadas, solo accesibles desde el comportamiento actual
    private float movimiento;
    private Vector2 direccion;

    //Referencia al Cuerpo Rígido localizado en el mismo Objeto de Juego
    private Rigidbody2D cuerpo;

    //Al Activarse: Se ejecuta una vez, antes de la primer actualización
    private void Start()
    {
        //Se obtiene el componente de cuerpo rígido, para almacenarlo en su variable y usarse
        cuerpo = GetComponent<Rigidbody2D>();

    }

    //Actualización: Se ejecuta constantemente, pero el intervalo puede variar
    private void Update()
    {
        //Se almacena el valor del input horizontal obtenido del jugador, luego se crea un vector que contenga este valor para x únicamente
        movimiento = Input.GetAxis("Horizontal");
        direccion = new Vector2(movimiento, 0f);
    }

    //Actualización Fija: Se ejecuta constantemente, pero su intervalo nunca varía. Se usa para el cálculo de colisiones
    private void FixedUpdate()
    {
        //Verifica que el objecto esté activo antes de intentar moverlo
        if (gameObject.activeSelf) {

            //Impulsa al cuerpo con el vector creado previamente, multiplicandolo por la velocidad de movimiento
            cuerpo.AddForce(direccion * velocidad);

        }
        
    }
}