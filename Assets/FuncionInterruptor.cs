using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FuncionInterruptor : MonoBehaviour
{

    //Variables locales
    private bool fueActivado = false;

    //Al ingresar en Trigger 2D: Se ejecuta cuando algo entra en contacto con este objeto
    void OnTriggerEnter2D(Collider2D contacto) {

        //Si colisionó con un jugador y aún no fue activado
        if (contacto.gameObject.tag == "Player" && !fueActivado) {

            //Registra el contacto por consola, cambia el color del interruptor a verde y cambia su estado a activo
            Debug.Log("Interruptor activado");
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            fueActivado = true;

        }
    }

    //Retorna si el interruptor fue activado o no
    public bool estadoActivacion() {

        return fueActivado;
        
    }
}
