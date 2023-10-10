using UnityEngine;

public class InteraccionRecolectables : MonoBehaviour
{

    //Referencias serializadas a componentes externos
    [SerializeField] public GameObject objetoInventario;

    //Variables locales y referencias
    private Collider2D colisionador;
    private SpriteRenderer sprite;
    private GestorInventario inventario;
    private PropiedadesItem propiedades;

    //Iniciar: Se ejecuta una sola vez antes de la primer actualización de frame
    void Start() {

        //Obtiene los componentes y los asigna a sus variables
        sprite = GetComponent<SpriteRenderer>();
        colisionador = GetComponent<Collider2D>();

        //Obtiene el componente de propiedades y lo asigna a su variable
        propiedades = GetComponent<PropiedadesItem>();

        //Obtiene el componente de inventario del objeto serializado y lo asigna a la variable inventario
        objetoInventario.gameObject.TryGetComponent<GestorInventario>(out inventario);

        //Si la variable de inventario es nula...
        if (inventario == null)
        {
            //Informa el error de obtención por consola
            Debug.LogError("[INFO/ERROR]: No se encontró el componente GestorInventario en el objeto: " + objetoInventario.gameObject.name + " ");

        }

    }

    //Al ingresar a un Trigger 2D: Se dispara cuando el objeto en cuestión colisiona con el Trigger de otro objeto
    void OnTriggerEnter2D(Collider2D contacto) {

        //Si el objeto con el cual se colisionó está etiquetado como Jugador...
        if (contacto.gameObject.CompareTag("Player")) {

            //Asigna el id del item a insertar en la variable temporal, además prepara el nuevo objeto a insertar
            string nuevoIdentificador = propiedades.retornarIdentificador();
            GameObject nuevoObjecto = this.gameObject;

            //Llama al método de recolectar, suministrando el nuevo objeto y el identificador como parámetros
            Recolectar(nuevoIdentificador, nuevoObjecto);

        }

    }

    //Recolectar: Se encarga de eliminar el objecto recogido y registrar dicha acción por consola. Recibe un string como id y un objeto a
    //insertar en el inventario
    private void Recolectar(string id, GameObject objeto) {

        //Llama al método de Agregar item dentro del inventario, pasando la identificación y el nuevo objeto a añadir
        inventario.AgregarItem(id, objeto);

        //Registra por consola que el item ha sido recolectado correctamente
        Debug.Log("Item recolectado: " + gameObject.transform.name + " ");
        
        //Desactiva tanto el sprite como el colisionador del item para que no pueda verse o recolectarse dos veces
        sprite.enabled = false;
        colisionador.enabled = false;

        //Coloca el objeto bajo el objeto de inventario, transformando a este último en su padre
        this.gameObject.transform.SetParent(objetoInventario.gameObject.transform);

    }
    
}
