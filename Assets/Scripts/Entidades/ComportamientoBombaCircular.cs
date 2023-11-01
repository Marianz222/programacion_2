using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Comportamiento para Proyectiles de tipo "Spherical Bomb". Hereda de Comportamiento Proyectil
public class ComportamientoBombaCircular : ComportamientoProyectil
{

    //Referencias a archivos de configuracion
    [Header("Referencias a Configuraciones")]
    [SerializeField]
    [Tooltip("Una referencia al objeto scripteable que contiene los datos de configuracion de la entidad")]
    private ConfiguracionProyectil datos;

    //Referencias a componentes adicionales
    private ListadoEtiquetas listaEtiquetas;

    //Sobre-escritura del método "Mover Proyectil". Revisar la clase de la que deriva para ver su descripción
    protected override void moverProyectil()
    {

        //No implementa este método

    }

    //Sobre-escritura del método "Inicializar Variables". Revisar la clase de la que deriva para ver su descripción
    protected override void inicializarVariables()
    {

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

    }

}
