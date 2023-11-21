using System.Collections;
using UnityEngine;

public class FuncionPuertaBloqueada : MonoBehaviour
{

    //Referencias serializadas a componentes externos
    [Header("Referencia a Componentes")]
    [SerializeField] private ConfiguracionPuertaBloqueada configuracionPuerta;
    [SerializeField] private ConfiguracionRecolectables itemSeleccionado;
    [SerializeField] private GameObject objetoInventario;
    [SerializeField] private GameObject puerta;
    [SerializeField] private GameObject objetoBoton;

    //Variables locales y referencias privadas
    private GestorInventario inventario;
    private string identificacionItemRequerido;
    private Sprite spriteItem;
    private SpriteRenderer renderizadorSprite;

    //Iniciar: Se llama una única vez antes de la primera actualización de frame
    void Start() {

        //Asigna tanto los valores que dependen de los objetos scripteables de configuración, asi como los componentes
        //obtenidos a variables locales
        identificacionItemRequerido = itemSeleccionado.Identificador;
        inventario = objetoInventario.GetComponent<GestorInventario>();
        spriteItem = itemSeleccionado.Sprite;
        renderizadorSprite = puerta.GetComponent<SpriteRenderer>();
        renderizadorSprite.sprite = spriteItem;

    }

    //Al ingresar a un Trigger 2D: Se ejecuta cuando un cuerpo externo ingresa al trigger de este objeto
    void OnTriggerEnter2D(Collider2D contacto) {

        //Si el objeto que está colisionando con este es de Tipo Jugador y la puerta puede abrirse...
        if (contacto.gameObject.CompareTag("Player") && puedeAbrirse()) {

            //Placeholder

        }

    }

    //Puede Abrirse: Devuelve true si el jugador cumple con todos los requerimientos para abrir la puerta. Se dispara al tocar el botón
    public bool puedeAbrirse() {

        //Si el item requerido para abrir la puerta se encuentra dentro del inventario (no es nulo)...
        if (inventario.obtenerItem(identificacionItemRequerido) != null)
        {

            //Registra por consola cual es el nombre del item que se requiere para abrir la puerta
            Debug.Log("Item requerido: " + inventario.obtenerItem(identificacionItemRequerido).gameObject.name + " ");

        }
        
        //Si el inventario contiene el ítem que se requiere...
        if (inventario.hayItem(identificacionItemRequerido)) {

            //Se dispara el procedimiento de apertura y se retorna verdadero
            ProcedimientoApertura(identificacionItemRequerido);
            return true;

        }

        //Se retorna false si no se encuentra el item
        return false;

    }

    //Procedimiento Apertura: Recibe un ID. Se encarga de llamar al método que eliminará la pared. Elimina el item del
    //inventario y ejecuta lo necesario para abrir la puerta
    private void ProcedimientoApertura(string id) {

        //Si la puerta debe desbloquearse instantáneamente...
        if (configuracionPuerta.DesbloqueoInstantaneo) {

            //Se elimina la puerta
            eliminarBloqueo(id);

        } else {

            //Sino significa que la puerta debe desbloquearse en "X" tiempo, se llama a un inicio de corrutina
            StartCoroutine(nameof(aperturaTemporizada));

        }

    }

    //Eliminar Bloqueo: Destruye la puerta y recibe el ID como string, para retirar el item del inventario
    private void eliminarBloqueo(string id) {

        inventario.retirarItem(id);
        Destroy(inventario.obtenerItem(id));
        puerta.gameObject.SetActive(false);

    }

    //Apertura Temporizada: Corrutina que recibe como parámetro el ID del item para realizar el desbloqueo y el tiempo para la espera
    IEnumerator aperturaTemporizada(int tiempo, string id) {

        yield return new WaitForSeconds(tiempo);

        eliminarBloqueo(id);

    }

}
