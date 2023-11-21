using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControladorJugador : MonoBehaviour
{
    //Objetos Scripteables / archivos de configuracion
    [Header("Archivos de Configuración")]
    [SerializeField] private ConfiguracionJugador datos;

    //Eventos disponibles
    [Header("Eventos Disponibles")]

    //Parámetros: Vida (int), Valor de modificacion (int), accion de modificación [addition/reduction] (string)
    [SerializeField] private UnityEvent<int, int, string> alModificarVida;

    //Parámetros: Energía <int>
    [SerializeField] private UnityEvent<int> alIncrementarEnergia;

    //Parámetros: Energía <int>
    [SerializeField] private UnityEvent<int> alDecrementarEnergia;
    [SerializeField] private UnityEvent alMorir;
    [SerializeField] private UnityEvent alGanar;
    [SerializeField] private UnityEvent alReaparecer;
    [SerializeField] private UnityEvent alModificarCuboides;
    [SerializeField] private UnityEvent AlUsarHabilidadPrincipal;
    [SerializeField] private UnityEvent AlUsarHabilidadSecundaria;

    //Referencias a componentes y activos generales
    private SpriteRenderer renderizadorSprite;
    private Sprite[] conjuntoSprites;

    //Referencias a componentes de tipo "Script"
    private MovimientoJugador funcionMovimiento;
    private SaltoJugador funcionSalto;

    //Referencias a componentes de tipo "Código de Tecla"
    private KeyCode teclaHabilidadPrincipal;
    private KeyCode teclaHabilidadSecundaria;

    //Variables locales y privadas
    private int vida;
    private int energia;
    private float velocidadMovimiento;
    private float alturaSalto;
    private bool estaMuerto = false;
    private bool esInvulnerable = false;
    private bool tieneEnergiaInfinita = false;
    private bool puedeCurarse = true;

    //Variables de datos persistentes
    private int cuboides;

    //Despertar: Se llama en la creación del script y antes de Iniciar
    void Awake()
    {

        obtenerComponentes();

        //Se llama a recuperar los datos de configuracion
        obtenerConfiguracion();

        suscripcionEventos();

    }

    void Start() {

        inicializarVariables();

        StartCoroutine(regeneracionEnergetica(10, 1));

    }

    private void suscripcionEventos() {

        ComportamientoProyectil[] proyectiles = FindObjectsOfType<ComportamientoProyectil>();

        foreach (ComportamientoProyectil proyectil in proyectiles) {

            proyectil.alColisionarConProyectil.AddListener(() => dealDamage(25));

            //Debug.Log("Se suscribió a: " + proyectil.gameObject.name + " al evento de: " + this.gameObject.name);

        }

    }

    //Depurar Variables: Método interno que permite mostrar el estado de todas las variables del jugador en la consola
    private void depurarVariables() {

        Debug.Log("MOSTRANDO DATOS");

        Debug.Log("Vida: " + vida);
        Debug.Log("Energia: " + energia);
        Debug.Log("Velocidad: " + velocidadMovimiento);
        Debug.Log("Fuerza Salto: " + alturaSalto);
        Debug.Log("esta Muerto: " + estaMuerto);
        Debug.Log("Es Invulnerable: " + esInvulnerable);
        Debug.Log("Energia Infinita: " + tieneEnergiaInfinita);
        Debug.Log("puede Curarse: " + puedeCurarse);

        Debug.Log("Cuboides: " + cuboides);

    }

    void OnCollisionEnter2D(Collision2D contacto) {

        if (contacto.gameObject.CompareTag("Damage Source")) {

            dealDamage(20);

        }

    }

    private void obtenerComponentes()
    {

        //Se obtienen los componentes y se almacenan en las variables, pero solo si estos componentes no son nulos
        this.gameObject.TryGetComponent<SpriteRenderer>(out renderizadorSprite);
        this.gameObject.TryGetComponent<MovimientoJugador>(out funcionMovimiento);
        this.gameObject.TryGetComponent<SaltoJugador>(out funcionSalto);

    }

    //Obtener Configuración: Método que obtiene los valores desde el objeto scripteable de configuración suministrado y los asigna a las
    //Variables locales del script
    private void obtenerConfiguracion()
    {

        conjuntoSprites = new Sprite[3];

        //Se obtienen las variables básicas
        vida = datos.VidaMaxima;
        energia = datos.EnergiaMaxima;
        velocidadMovimiento = datos.VelocidadMovimiento;
        alturaSalto = datos.AlturaSalto;

        //Se obtienen las teclas
        teclaHabilidadPrincipal = datos.TeclaHabilidadPrincipal;
        teclaHabilidadSecundaria = datos.TeclaHabilidadSecundaria;

        //Se obtienen los datos multimedia
        conjuntoSprites[0] = datos.SpriteCubo;
        conjuntoSprites[1] = datos.SpriteSierra;
        conjuntoSprites[2] = datos.SpriteAracnido;

    }

    //Inicializar Variables: Le asigna el valor por defecto a las variables utilizadas en el script
    private void inicializarVariables() {

        cuboides = LocalDataManager.Instancia.cargarDatoEntero("Cuboides");

        renderizadorSprite.sprite = conjuntoSprites[0];

    }

    //Causar Daño: Este método recibe la cantidad de daño a causar por parámetro, se encarga de restar dicho valor a la vida del jugador
    //Si la invulnerabilidad está activa, esta función no hará nada. Si la vida baja de cero, se ejecuta el procedimiento de eliminación
    public void dealDamage(int amount) {

        //Si el jugador es invulnerable...
        if (esInvulnerable) {

            //Sale del método
            return;

        }

        //Si la vida es menor o igual a 0...
        if (vida <= 0) {

            //Llama a la función de eliminar jugador
            eliminar();

        }

        //Si se superan las condiciones anteriores, se resta la cantidad de daño a la vida
        vida -= amount;

        alModificarVida.Invoke(vida, amount, "addition");

    }

    //Curar: Recupera vida al jugador usando el valor pasado por parámetro, este se sumará a la vida actual total
    public void curar(int cantidad) {

        //Si no puede curarse...
        if (!puedeCurarse) {

            //Sale del método
            return;

        }

        //Si supera las condiciones anteriores, se suma la cantidad a la vida actual
        vida += cantidad;

        alModificarVida.Invoke(vida, cantidad, "addition");

    }

    //Decrementar Energia [Base]: Retira 1 punto de energía al jugador, solamente si NO tiene energía infinita activada. Este
    //método no recibe parámetros a diferencia de su sobrecarga
    public void decrementarEnergia() {

        //Si el jugador tiene energía infinita...
        if (tieneEnergiaInfinita)
        {

            //Sale del método
            return;

        }

        //Retira un punto de energía
        energia--;

        alDecrementarEnergia.Invoke(1);

    }

    //Decrementar Energia [Sobrecarga]: Retira la cantidad de puntos de energia que se suministren por parámetro al jugador, aunque solo
    //Se ejecuta cuando la energía infinita no está activada
    public void decrementarEnergia(int cantidad) {

        //Si el jugador tiene energía infinita...
        if (tieneEnergiaInfinita) {

            //Sale del método
            return;

        }

        //Le resta la cantidad a la energía y guarda el resultado
        energia -= cantidad;

        alDecrementarEnergia.Invoke(cantidad);

    }

    //Incrementar Energia [Base]: Suma 1 punto a la energía del jugador, no recibe parámetros
    public void incrementarEnergia() {

        //Suma 1 a la energia
        energia++;

        //Invoca el evento al modificar la energia
        alIncrementarEnergia.Invoke(1);

    }

    //Incrementar Energia [Sobrecarga]: Suma el valor suministrado por parámetro a la energía del jugador
    public void incrementarEnergia(int cantidad)
    {

        //Suma el valor a la energia actual
        energia += cantidad;

        //Invoca el evento al modificar la energia
        alIncrementarEnergia.Invoke(cantidad);

    }

    //Eliminar: Encargado de desactivar las funciones del jugador y cambiar las banderas cuando este muere
    public void eliminar() {

        funcionSalto.enabled = false;
        funcionMovimiento.enabled = false;

        estaMuerto = true;

        alMorir.Invoke();

    }

    public void dispararVictoria() {

        funcionSalto.enabled = false;
        funcionMovimiento.enabled = false;

        alGanar.Invoke();

    }

    //Reaparecer: Método en construcción, no usado actualmente...
    public void reaparecer() {

        gameObject.transform.position = new Vector2(0.0f, 0.0f);

        funcionSalto.enabled = true;
        funcionMovimiento.enabled = true;

        estaMuerto = false;

        alReaparecer.Invoke();

    }

    public void incrementarCuboides(int cantidad) {

        cuboides += cantidad;

        LocalDataManager.Instancia.almacenarDatoEntero("Cuboides", cuboides);
        LocalDataManager.Instancia.guardarDatos();

        alModificarCuboides.Invoke();

    }

    public void borrarCuboides() {

        cuboides = 0;

        LocalDataManager.Instancia.borrarDatoAlojado("Cuboides");
        LocalDataManager.Instancia.guardarDatos();

        alModificarCuboides.Invoke();

        Debug.Log("[INFO/DEBUG] Se ha reiniciado el conteo de Cuboides almacenado en disco");

    }

    IEnumerator regeneracionEnergetica(int intervalo, int cantidadRegenerada) {

        while (true) {

            yield return new WaitForSeconds(intervalo);

            if (energia < datos.EnergiaMaxima) {

                energia++;

                alIncrementarEnergia.Invoke(cantidadRegenerada);

            }

        }

    }

    /////////////////////////////////////////////////////////////////////////////////////////////
    
    //Retornar Vida: Devuelve la vida actual
    public int retornarVida() {

        return vida;

    }

    //Retornar Vida Maxima: Devuelve la cantidad de vida máxima que se puede almacenar
    public int retornarVidaMaxima() {

        return datos.VidaMaxima;

    }

    //Retornar Energia: Devuelve la energia actual
    public int retornarEnergia() {

        return energia;

    }

    //Retornar Energia Maxima: Devuelve la cantidad de energia máxima que se puede almacenar
    public int retornarEnergiaMaxima() {

        return datos.EnergiaMaxima;

    }

    //Retornar Velocidad Movimiento: Devuelve la velocidad de movimiento
    public float retornarVelocidadMovimiento() {

        return velocidadMovimiento;

    }

    //Retornar Altura Salto: Devuelve la fuerza de salto
    public float retornarAlturaSalto() {

        return alturaSalto;

    }

    //Fue Eliminado: Devuelve verdadero o falso dependiendo de si el jugador ha sido eliminado
    public bool fueEliminado() {

        return estaMuerto;

    }

    //Retornar Tecla Habilidad Principal: Devuelve la tecla con la que se activa la habilidad principal (Jump, Sawrush, Flick)
    public KeyCode retornarTeclaHabilidadPrincipal() {

        return teclaHabilidadPrincipal;

    }

    //Retornar Tecla Habilidad Secundaria: Devuelve la tecla con la que se activa la habilidad secundaria (Dash, EMP, Websling)
    public KeyCode retornarTeclaHabilidadSecundaria() {

        return teclaHabilidadSecundaria;

    }

    public int retornarCuboides() {

        return cuboides;

    }

}
