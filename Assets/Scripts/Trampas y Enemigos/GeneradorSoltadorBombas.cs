using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorSoltadorBombas : MonoBehaviour
{

    //Referencias a objetos de juego
    [Header("Referencias a Objetos de Juego")]
    [SerializeField] private GameObject origenBomba;
    [SerializeField] private GameObject[] conjuntoPrefabs;
    [SerializeField] private GameObject contenedorProyectiles;

    //Configuración de variables simples
    [Header("Configuración de Variables")]

    [SerializeField]
    [Range(2, 5)] 
    private int intervaloTiempo;

    [SerializeField]
    [Range(1, 8)]
    private int tiempoInicial;
    
    [SerializeField] private int proyectilesMaximos;

    //Variables TDA
    private List<GameObject> listaProyectiles;

    //Despertar: Se llama una única vez al crear el objeto
    void Awake() {

        //Llama a la inicialización de los proyectiles
        inicializarProyectiles();

    }

    //Iniciar: Se llama una vez antes de la primer actualización de frame
    void Start()
    {

        //Llama al bucle que usa InvokeRepeating para generar objetos constantemente
        bucleGeneracionBomba();

    }

    //Inicializar Proyectiles: Método que inicializa la lista de proyectiles, itera sobre la cantidad máxima de proyectiles fijada y
    //crea instancias de los proyectiles. Adicionalmente configura dichas nuevas instancias, las desactiva y fija su objeto padre para
    //Almacenarlas en el contenedor de proyectiles
    private void inicializarProyectiles()
    {

        //Se inicializa la lista de proyectiles
        listaProyectiles = new List<GameObject>();

        //Bucle que itera sobre la cantidad de proyectiles establecida para instanciarlos
        for (int i = 0; i < proyectilesMaximos; i++)
        {

            //Se elige un prefab de entre los contenidos en el arreglo, usando el índice del mismo como delimitante. El elegido se
            //guardará en la variable creada para usarse
            GameObject prefabSeleccionado = conjuntoPrefabs[Random.Range(0, conjuntoPrefabs.Length)];

            //Guarda la referencia al nuevo proyectil instanciado
            GameObject nuevoProyectil = Instantiate(prefabSeleccionado);

            //Fija el padre del proyectil como el contenedor de proyectiles, desactiva el proyectil y lo añade a la lista
            nuevoProyectil.gameObject.transform.SetParent(contenedorProyectiles.gameObject.transform);
            nuevoProyectil.SetActive(false);

            //Añade el proyectil creado a la lista
            listaProyectiles.Add(nuevoProyectil);

        }

    }

    //Llama a la generación en bucle del método de generación de bombas
    private void bucleGeneracionBomba() {

        //Llama al método de generación cada cierto tiempo. Dicho tiempo y el periodo inicial son configurables
        InvokeRepeating(nameof(generarBomba), tiempoInicial, intervaloTiempo);

    }

    //Desactiva la invocación constante del método de generación de bombas
    private void cancelarGeneracion() {

        //Cancela el llamado constante a generar bomba
        CancelInvoke(nameof(generarBomba));

    }

    //Disparar Bomba: Obtiene un proyectil disponible de la piscina (pool), si encuentra uno fija su posición inicial y lo activa
    //La lógica de movimiento depende del proyectil instanciado (Es decir, del prefabricado usado)
    private void generarBomba() {

        //Guarda el minimo y el máximo para el generador de numero aleatorio
        int minimoRandom = 0;
        int maximoRandom = 4;

        //Genera el número aleatorio y lo almacena
        int numeroAleatorio = Random.Range(minimoRandom, maximoRandom);

        //Si el número es mayor a 2 (50% probabilidad)
        if (numeroAleatorio > 2) {

            //Sale de la función
            return;

        }

        //Almacena referencia al proyectil obtenido de la piscina
        GameObject nuevoProyectil = obtenerBombaDisponible();

        //Si se encontró un proyectil disponible...
        if (nuevoProyectil != null)
        {

            //Posiciona el proyectil en su origen, lo activa y registra por consola
            nuevoProyectil.transform.position = origenBomba.transform.position;
            nuevoProyectil.SetActive(true);
            Debug.Log("[DEBUG/INFO]: Proyectil disparado!");

            //Sino...
        }
        else
        {

            //No se encontró un proyectil disponible: Se registra por consola con el marcador de advertencia
            Debug.LogWarning("[DEBUG/WARNING]: La entidad " + gameObject.name + " se quedó sin proyectiles para disparar");

        }

    }

    //Obtener Bomba Disponible: Método que revisa la piscina de proyectiles iterando sobre cada elemento de la lista.
    //Si encuentra un objeto disponible para usarse, lo retorna. Si no lo encuentra, retorna un puntero nulo
    public GameObject obtenerBombaDisponible() {

        //Para cada objeto de tipo proyectil en la lista...
        foreach (GameObject proyectil in listaProyectiles)
        {

            //Si el proyectil no está activo...
            if (!proyectil.activeInHierarchy)
            {

                //Se retorna el proyectil
                return proyectil;

            }

        }

        //Se retorna nulo, ya que no se encontró un proyectil disponible para reutilizar
        return null;

    }
}
