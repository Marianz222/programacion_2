using UnityEngine;

public class FuncionBotonInteraccion : MonoBehaviour
{
    //Referencias serializadas a componentes
    [Header("Referencias a Componentes")]
    [SerializeField] public Sprite spriteActivacion;

    //Referencias a componentes
    private SpriteRenderer sprite;

    //Variables locales
    private bool fueActivado = false;

    //Iniciar: Se llama antes de la primera actualización de frame
    void Start()
    {
        //Almacena los componentes obtenidos en las variables
        sprite = GetComponent<SpriteRenderer>();

    }

    //Activar Boton: Verifica si el boton ya fue activado, de ser esto falso cambia el sprite por el de activación y cambia la bandera
    public void activarBoton() {

        //Si no fue activado...
        if (!fueActivado) {

            //Cambia el sprite, la variable bool y registra por consola
            sprite.sprite = spriteActivacion;
            fueActivado = true;
            Debug.Log("[INFO/DEBUG]: Puerta desbloqueada");

        }

    }
}
