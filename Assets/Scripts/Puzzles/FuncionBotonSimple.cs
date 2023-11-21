using UnityEngine;
using UnityEngine.Events;

public class FuncionBotonSimple : MonoBehaviour
{
    //Referencias serializadas a componentes
    [Header("Referencias a Componentes")]
    [SerializeField] private Sprite spriteInactivo;
    [SerializeField] private Sprite spriteActivo;

    [Header("Eventos Disponibles")]
    [SerializeField] private UnityEvent AlPresionarBoton;

    //Referencias a componentes
    private SpriteRenderer sprite;

    //Variables locales
    private bool fueActivado = false;

    //Iniciar: Se llama antes de la primera actualización de frame
    void Start()
    {
        //Almacena los componentes obtenidos en las variables
        sprite = GetComponent<SpriteRenderer>();

        sprite.sprite = spriteInactivo;

    }

    void OnCollisionEnter2D(Collision2D contacto) {

        if (contacto.gameObject.CompareTag("Player")) {

            activarBoton();

        }

    }

    //Activar Boton: Verifica si el boton ya fue activado, de ser esto falso cambia el sprite por el de activación y cambia la bandera
    public void activarBoton() {

        //Si no fue activado...
        if (!fueActivado) {

            //Cambia el sprite, la variable bool y registra por consola
            sprite.sprite = spriteActivo;
            fueActivado = true;

            AlPresionarBoton.Invoke();

            Debug.Log("[INFO/DEBUG]: Boton activado");

        }

    }
}
