using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase plantilla para crear el comportamiento de proyectiles. Hereda de MonoBehaviour
public abstract class ComportamientoProyectil : MonoBehaviour
{

    //Variables de tipo Componente
    protected GameObject dueño;
    protected SpriteRenderer sprite;
    protected Collider2D colisionador;
    protected ParticleSystem sistemaParticulas;
    protected Rigidbody2D cuerpo;

    //Variables simples
    protected int daño;
    protected int velocidad;
    protected int multiplicadorVelocidad;
    protected float tiempoInmunidad;
    protected int cantidadParticulas;
    protected string direccion;
    private float tiempoDestruccion = 0.2f;

    //MÉTODOS ABSTRACTOS
    /////////////////////////////////////////////////////////////////////////////

    //Mover Proyectil [Opcional]: Sobreescribir para que el proyectil se mueva hacia una dirección fijada constantemente
    protected abstract void moverProyectil();

    //Obtener Componentes [Requerido]: Sobreescribir para obtener la referencia necesaria para que funcionen los componentes enlazados
    protected abstract void obtenerComponentes();

    //Obtener Componentes [Requerido]: Sobreescribir para asignar los valores a las variables inicializadas y obtenidas desde esta clase
    protected abstract void inicializarVariables();

    /////////////////////////////////////////////////////////////////////////////

    //Iniciar: Se llama antes de la primera actualización de frame
    void Start()
    {

        //Llamada a inicializar las variables heredadas y las locales
        inicializarVariables();

        //Llamada a fijar las referencias a componentes heredados y locales
        obtenerComponentes();

    }

    //Actualizar: Se llama constantemente cada frame
    void Update()
    {

        //Se llama al método de movimiento del proyectil
        moverProyectil();

    }

    //Al entrar a un Trigger 2D: Se llama cuando el collider entra en contacto
    void OnTriggerEnter2D(Collider2D contacto) {

        //Si el objeto con el que se colisionó es el dueño del proyectil...
        if (contacto.gameObject == dueño)
        {

            //Sale de la función: No hay colisión con el dueño
            return;

        }

        //Llamada a la destrucción del proyectil
        destruirProyectil();

    }

    //Destruir Proyectil: Llama a la corrutina para desactivar el proyectil
    private void destruirProyectil() {

        //Eliminada la llamada a la corrutina por un error que no supe como reparar
        gameObject.SetActive(false);
        //StartCoroutine(nameof(desactivarProyectil));

    }

    //Efecto Explosion: Método que activa la emisión de particulas, emitiendo la cantidad especificada usando la variable de cantidad
    private void efectoExplosion() {

        //Emite las partículas de colisión, que dependen de la variable usada
        sistemaParticulas.Emit(cantidadParticulas);

    }

    //Desactivar Proyectil [Corrutina]: Desactiva los visuales y físicas del proyectil, ejecuta el efecto de explosión y espera cierto
    //tiempo para desactivar el objeto de la escena. Usada para que las partículas se puedan visualizar antes de que el objeto se desactive
    IEnumerator desactivarProyectil() {

        //El sprite y el cuerpo se desactivan, se llama a la función que muestra las particulas
        sprite.enabled = false;
        cuerpo.simulated = false;
        efectoExplosion();

        //Se espera el tiempo de destrucción especificado...
        yield return new WaitForSeconds(tiempoDestruccion);

        //Se desactiva el objeto
        gameObject.SetActive(false);

    }


}
