using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HabilidadSecundariaCubo : MonoBehaviour
{

    //Campos de referencia a objetos de configuración (Scriptable Objects)
    [Header("Referencias a Configuraciones")]
    [SerializeField] private ConfiguracionJugador datosJugador;

    //Configuración de variables
    [Header("Variables Configurables")]
    [SerializeField] private int fuerzaImpulso = 3;
    [SerializeField] private int tiempoEnfriamiento = 3;

    //Referencias a objetos y componentes externos
    [Header("Referencias a Objetos Externos")]
    [SerializeField] private GameObject objetoInterfaz;

    //Enlaces a eventos
    [Header("Eventos Disponibles")]
    [SerializeField] private UnityEvent alUsarHabilidadSecundaria;

    //Variables locales y privadas
    private Rigidbody2D cuerpo;
    private KeyCode teclaActivacion;
    private bool puedeUsarHabilidad;
    private bool usandoHabilidad = false;
    private bool enfriamientoActivo = false;
    private bool estaMoviendose;
    private GestorHUD scriptInterfaz;
    private Vector2 direccionMovimiento;
    private int multiplicadorVelocidad = 150;
    private TrailRenderer rastro;

    //Iniciar: Se llama antes de la primera actualización de frame
    void Start() {

        //Obtiene los componentes de cuerpo rígido, script de interfaz
        //tecla de activación (usando la configuración del jugador) y el rastro
        cuerpo = GetComponent<Rigidbody2D>();
        scriptInterfaz = objetoInterfaz.GetComponent<GestorHUD>();
        teclaActivacion = datosJugador.TeclaHabilidadSecundaria;
        rastro = GetComponent<TrailRenderer>();

    }

    //Actualizar: Se llama en cada frame
    void Update()
    {

        //Almacena el valor de entrada horizontal (1 o -1 dependiendo al dirección)
        direccionMovimiento = new Vector2(Input.GetAxis("Horizontal"), 0);

        //Detecta si el jugador se está moviendo a su máxima capacidad o no, de ser así permitirá usar la habilidad
        estaMoviendose = (direccionMovimiento.x == 1 || direccionMovimiento.x == -1);

        //Si la tecla de activación está siendo presionada, la energía es mayor a 0, el jugador se está moviendo, no hay enfriamiento
        //activo y la habilidad no se está usando actualmente...
        if (Input.GetKeyDown(teclaActivacion) && (scriptInterfaz.retornarEnergia() > 0) && estaMoviendose  && !enfriamientoActivo && !usandoHabilidad) {

            //Llama al inicio de la corrutina de activar habilidad
            StartCoroutine(nameof(activarHabilidad));

        }

        //DEBUG: Si el jugador pulsa la tecla Q...
        if (Input.GetKeyDown(KeyCode.Q)) {

            //Añade energía al jugador, usado para Debugging. Se registra por consola
            scriptInterfaz.modificarEnergiaActual("add");
            Debug.Log("[INFO/DEBUG]: Se añadió un punto de energía!");

        }

    }

    //Activar Habilidad: Corrutina que permite el dash/impulso. Tiene 2 puntos de parada donde se espera para cancelar la emisión
    //del componente de rastro, y después de eso otra parada en la que se espera el tiempo de enfriamiento para evitar impulsos repetidos
    IEnumerator activarHabilidad() {

        //Se ajustan los booleanos necesarios: La habilidad se está usando, el enfriamiento se ha activado y se activó la emisión del rastro
        usandoHabilidad = true;
        enfriamientoActivo = true;
        rastro.emitting = true;

        //Se almacena la escala de gravedad, y se establece en 0 (haciendo que durante el dash el jugador no tenga gravedad)
        float escalaGravedad = cuerpo.gravityScale;
        cuerpo.gravityScale = 0;

        //Se añade la fuerza de impulso, depende de la dirección, de la fuerza de impulso y además del multiplicador de velocidad
        cuerpo.AddForce(direccionMovimiento * fuerzaImpulso * multiplicadorVelocidad, 0);
        
        //Registra por consola que la habilidad se ejecutó correctamente
        Debug.Log("[INFO/DEBUG]: Ejecutada la habilidad: Impulso Horizontal");

        //Se invoca el evento
        alUsarHabilidadSecundaria.Invoke();

        //Recupera la gravedad original y cambia el uso de la habilidad a falso
        cuerpo.gravityScale = escalaGravedad;
        usandoHabilidad = false;

        //Primera parada: Se esperan 0.3 segundos
        yield return new WaitForSeconds(0.3f);

        //La emisión del rastro es desactivada
        rastro.emitting = false;

        //Segunda parada: Se espera el tiempo de enfriamiento definido en la configuración de variables
        yield return new WaitForSeconds(tiempoEnfriamiento);

        //Enfriamiento activo cambia a falso tras esperar el tiempo anteriormente mencionado, para permitir re-activar el dash
        enfriamientoActivo = false;

    }
}
