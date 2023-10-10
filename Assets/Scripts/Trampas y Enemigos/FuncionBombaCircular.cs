using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionBombaCircular : MonoBehaviour
{

    //Referencias a componentes
    private SpriteRenderer spriteBomba;
    private Rigidbody2D cuerpoBomba;
    private CircleCollider2D colisionadorBomba;
    private ParticleSystem sistemaParticulas;
    private ListadoEtiquetas listaEtiquetas;

    //Variables locales
    private float tiempoInmunidadColision = 0.1f;
    private float tiempoAutodestruccion = 2;
    private bool haDetonado = false;

    //Iniciar: Se llama antes de la primera actualización de frame
    void Start() {

        //Obtiene todos los componentes y los asigna a sus variables
        spriteBomba = GetComponent<SpriteRenderer>();
        sistemaParticulas = GetComponent<ParticleSystem>();
        cuerpoBomba = GetComponent<Rigidbody2D>();
        colisionadorBomba = GetComponent<CircleCollider2D>();

    }

    //Al colisionar en 2D: Se llama cuando el objeto actual colisiona con [contacto]
    void OnCollisionEnter2D (Collision2D contacto) {

        //Inicia la corrutina del procedimiento de colision, suministrando el contacto como parámetro para la misma
        StartCoroutine(procedimientoColision(contacto)); ;

    }

    //Espera cierto tiempo antes de verificar si la bomba ha colisionado con algo. De ser asi, elimina la misma y genera la explosion
    IEnumerator procedimientoColision(Collision2D contacto) {

    //Mientras no haya detonado
    while (!haDetonado) {

            //Espera [tiempoInmunidadColision] segundos antes de reanudar la corrutina
            yield return new WaitForSeconds(tiempoInmunidadColision);

            //Si [contacto] contiene un Listado de Etiquetas en su objeto y está etiquetado como un Objeto Solido...
            if (contacto.gameObject.TryGetComponent<ListadoEtiquetas>(out listaEtiquetas) && listaEtiquetas.etiquetadoObjetoSolido())
            {

                //Desactiva el sprite y el cuerpo rígido de la bomba, emite partículas, cambia la bandera de detonación a true y
                //llama al comienzo de la corrutina para borrar la instancia del objeto, suministrando [contacto] como parámetro
                spriteBomba.enabled = false;
                cuerpoBomba.simulated = false;
                sistemaParticulas.Emit(30);
                haDetonado = true;
                StartCoroutine(borrarInstancia(contacto));

            }

        }

    }

    //Espera [tiempoAutodestruccion] segundos para destruir la instancia del Objeto actual
    IEnumerator borrarInstancia(Collision2D contacto) {

        //Espera el tiempo suministrado para continuar
        yield return new WaitForSeconds(tiempoAutodestruccion);

        //Destruye el objeto de juego
        Destroy(this.gameObject);

    }
}
