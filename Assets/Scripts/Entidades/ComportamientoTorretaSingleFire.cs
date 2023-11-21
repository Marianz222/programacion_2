using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoTorretaSingleFire : MonoBehaviour
{

    //Referencias a Componentes Externos
    [Header("Referencias a Componentes")]
    [SerializeField] private GameObject contenedorProyectiles;
    [SerializeField] private GameObject prefabProyectil;
    [SerializeField] private GameObject origenProyectil;

    //Configuracion de objetos scripteables
    [Header("Archivos de Configuración")]
    [SerializeField] ConfiguracionTorreta datosTorreta;

    //Referencias privadas a componentes
    private ParticleSystem sistemaParticulas;

    //Variables locales y privadas
    private List<GameObject> listaProyectiles;


    private int proyectilesMaximos;
    private int tiempoInicial;
    private int tiempoIntervalo;

    //Despertar: Se ejecuta cuando se inicializa el objeto
    void Awake() {

        //Llama a la función que obtiene los datos del objeto scripteable usado
        recuperarDatos();

        //Llama a la inicialización de los proyectiles
        inicializarProyectiles();

    }

    //Iniciar: Se ejecuta antes de la primera actualización de frame
    void Start() {

        StartCoroutine(suspenderFuncionamiento(1));

        //Se llama al ciclo para disparar los proyectiles, contiene una llamada a Invoke Repeating
        cicloDisparoProyectiles();

        sistemaParticulas = GetComponent<ParticleSystem>();

    }

    //Recuperar Datos: Obtiene los datos del archivo de configuración y los sincroniza con los de las variables locales
    private void recuperarDatos() {

        //Fija el valor de las variables locales teniendo en cuenta los valores del archivo de configuración
        proyectilesMaximos = datosTorreta.LimiteProyectiles;
        tiempoInicial = datosTorreta.TiempoActivacion;
        tiempoIntervalo = datosTorreta.IntervaloDisparo;

    }

    //Inicializar Proyectiles: Método que inicializa la lista de proyectiles, itera sobre la cantidad máxima de proyectiles fijada y
    //crea instancias de los proyectiles. Adicionalmente configura dichas nuevas instancias, las desactiva y fija su objeto padre para
    //Almacenarlas en el contenedor de proyectiles
    private void inicializarProyectiles() {

        //Se inicializa la lista de proyectiles
        listaProyectiles = new List<GameObject>();

        //Bucle que itera sobre la cantidad de proyectiles establecida para instanciarlos
        for (int i = 0; i < proyectilesMaximos; i++) {

            //Guarda la referencia al nuevo proyectil instanciado
            GameObject nuevoProyectil = Instantiate(prefabProyectil);
            TrailRenderer rastroProyectil = nuevoProyectil.gameObject.GetComponent<TrailRenderer>();

            //Fija el padre del proyectil como el contenedor de proyectiles y desactiva el proyectil
            nuevoProyectil.gameObject.transform.SetParent(contenedorProyectiles.gameObject.transform);
            rastroProyectil.enabled = false;

            StartCoroutine(apagarProyectil(nuevoProyectil));

            //Añade el proyectil creado a la lista
            listaProyectiles.Add(nuevoProyectil);

        }

    }

    //Ciclo Disparo Proyectiles: Genera proyectiles usando la función Invocar Repetidamente. Usa el tiempo inicial de generación,
    //el intervalo de generación y el prefabricado del proyectil a instanciar
    private void cicloDisparoProyectiles() {

        //Llama a la invocación continua del método disparar proyectil, con su intervalo y tiempo inicial
        InvokeRepeating(nameof(dispararProyectil), tiempoInicial, tiempoIntervalo);

    }

    //Disparar Proyectil: Obtiene un proyectil disponible de la piscina (pool), si encuentra uno fija su posición inicial y lo activa
    //La lógica de movimiento depende del proyectil instanciado (Es decir, del prefabricado usado)
    private void dispararProyectil() {

        //Almacena referencia al proyectil obtenido de la piscina
        GameObject nuevoProyectil = obtenerProyectilDisponible();

        sistemaParticulas.Emit(1);
        
        //Si se encontró un proyectil disponible...
        if (nuevoProyectil != null) {

            TrailRenderer rastroProyectil = nuevoProyectil.GetComponent<TrailRenderer>();

            //Posiciona el proyectil en su origen, lo activa y registra por consola
            nuevoProyectil.transform.position = origenProyectil.transform.position;
            nuevoProyectil.SetActive(true);
            rastroProyectil.enabled = true;

        //Sino...
        } else {

            //No se encontró un proyectil disponible: Se registra por consola con el marcador de advertencia
            Debug.LogWarning("[DEBUG/WARNING]: La entidad " + gameObject.name + " se quedó sin proyectiles para disparar");

        }

    }

    //Obtener Proyectil Disponible: Método que revisa la piscina de proyectiles iterando sobre cada elemento de la lista.
    //Si encuentra un objeto disponible para usarse, lo retorna. Si no lo encuentra, retorna un puntero nulo
    public GameObject obtenerProyectilDisponible() {

        //Para cada objeto de tipo proyectil en la lista...
        foreach (GameObject proyectil in listaProyectiles) {

            //Si el proyectil no está activo...
            if (!proyectil.activeInHierarchy) {

                //Se retorna el proyectil
                return proyectil;

            }

        }

        //Se retorna nulo, ya que no se encontró un proyectil disponible para reutilizar
        return null;

    }

    IEnumerator apagarProyectil(GameObject proyectil) {

        yield return new WaitForSeconds(1);

        proyectil.gameObject.SetActive(true);

    }

    IEnumerator suspenderFuncionamiento(int tiempo) {

        yield return new WaitForSeconds(tiempo);

    }

}
