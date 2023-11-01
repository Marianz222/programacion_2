using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Comportamiento para Proyectiles de tipo "Energy Bolt". Hereda de Comportamiento Proyectil
public class ComportamientoEnergyBolt : ComportamientoProyectil
{

    //Referencias a archivos de configuracion
    [Header("Referencias a Configuraciones")]
    [SerializeField]
    [Tooltip("Una referencia al objeto scripteable que contiene los datos de configuracion de la entidad")]
    private ConfiguracionProyectil datos;

    //Variables locales y privadas
    private TrailRenderer rastro;

    //Sobre-escritura del método "Inicializar Variables". Revisar la clase de la que deriva para ver su descripción
    protected override void inicializarVariables()
    {

        //Establece el dueño del proyectil. Para eso sube en jerarquía al objeto de contenedor y luego al objeto que lo disparó
        dueño = gameObject.transform.parent.gameObject.transform.parent.gameObject;

        //Se ajusta el multiplicador de velocidad
        multiplicadorVelocidad = 2;

        //Se obtienen todos los valores desde el archivo de configuración. Se asignan a las variables heredadas
        daño = datos.Daño;
        velocidad = datos.Velocidad * multiplicadorVelocidad;
        tiempoInmunidad = datos.TiempoInmunidad;
        cantidadParticulas = datos.NumeroParticulas;

    }

    //Sobre-escritura del método "Obtener Componentes". Revisar la clase de la que deriva para ver su descripción
    protected override void obtenerComponentes()
    {

        //Obtiene todos los componentes del objeto y los asigna a sus variables
        sprite = this.GetComponent<SpriteRenderer>();
        colisionador = this.GetComponent<Collider2D>();
        cuerpo = this.GetComponent<Rigidbody2D>();
        sistemaParticulas = this.GetComponent<ParticleSystem>();
        rastro = this.GetComponent<TrailRenderer>();

    }

    //Sobre-escritura del método "Mover Proyectil". Revisar la clase de la que deriva para ver su descripción
    protected override void moverProyectil() {

        //Crea el vector que controla la dirección de movimiento (Temporalmente asignado a izquierda por problemas)
        //Este método será mejorado cuando arreglue el problema que tuve con las enumeraciones, las cuales iba a usar para la dirección
        Vector2 direccionMovimiento = Vector2.left;

        //Aplica la velocidad al cuerpo
        cuerpo.velocity = direccionMovimiento * velocidad;

        //Este método iba a estar en "OnEnable" pero tuve muchos problemas en la implementación, por algún motivo
        //Si iba en OnEnable los proyectiles a veces no se borraban (no se desactivaban). Cuando encuentre el fallo voy a cambiar esto

    }

}
