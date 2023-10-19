using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ControladorVidaJugador : MonoBehaviour
{

    //Sección de configuración de variables
    [Header("Configuracion de Variables")]
    [SerializeField] public Vector2 puntoOrigen;

    //Sección de referencia a componentes externos
    [Header("Referencias a Componentes")]
    [SerializeField] GameObject objetoInterfaz;

    //Variables y referencias locales
    private GestorHUD scriptInterfaz;

    //Iniciar: Se ejecuta antes de la primera actualización de frame
    void Start() {

        //Intenta obtener el componente de Gestor Interfaz del objeto interfaz
        //Si lo consigue guarda ese valor en la variable correspondiente
        objetoInterfaz.TryGetComponent<GestorHUD>(out scriptInterfaz);

    }

    //Actualizar: Se llama constantemente cada frame
    void Update() {

        //Si la vida es igual o menor a 0
        if (scriptInterfaz.retornarVida() <= 0) {

            //Registra en consola que el jugador murió y lo desactiva
            Debug.Log("Fuiste eliminado. Perdiste!");
            this.transform.parent.gameObject.SetActive(false);

        }

    }

    //Al ingresar a un Trigger 2D: Se llama cuando el objeto entra en contacto con un trigger externo
    private void OnTriggerEnter2D(Collider2D contacto)
    {
        //Si el objeto con el cual se colisionó es una fuente de daño (tiene ese tag)
        if (contacto.gameObject.CompareTag("Damage Source") && (this.transform.parent.gameObject.activeSelf == true))
        {

            //Llama al procedimiento suministrando el Collider2D como parámetro (actúa primera versión)
            procedimientoDaño(contacto);

        }

    }

    //Al colisionar en 2D: Se llama cuando el objeto entra en contacto con un colisionador externo
    private void OnCollisionEnter2D(Collision2D contacto) {

        //Si el objeto con el cual se colisionó es una fuente de daño (tiene ese tag)
        if (contacto.gameObject.CompareTag("Damage Source") && (this.transform.parent.gameObject.activeSelf == true))
        {

            //Llama al procedimiento suministrando la Collision2D como parámetro (actúa su sobrecarga)      
            procedimientoDaño(contacto);

        }

    }

    //Si se detecta una colision entre el trigger de [contacto] y el jugador, procede destruyendo al mismo.
    private void procedimientoDaño(Collider2D contacto) {

        //Registra en consola que el jugador recibió daño
        Debug.Log("Recibiste daño");
        scriptInterfaz.dañar(20);

    }

    //Si se detecta una colision entre [contacto] y jugador, procede destruyendo al mismo. Sobrecarga del método original
    private void procedimientoDaño(Collision2D contacto) {

        //Registra en consola que el jugador recibió daño
        Debug.Log("Recibiste daño");
        scriptInterfaz.dañar(20);

    }

}
