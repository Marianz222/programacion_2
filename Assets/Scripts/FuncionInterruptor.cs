using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FuncionInterruptor : MonoBehaviour
{

    //Campos serializados para componentes
    [Header("Referencias a Componentes")]
    [SerializeField] public Sprite spriteActivacion;

    //Variables locales
    private bool fueActivado = false;

    //Referencias a componentes
    private SpriteRenderer sprite;
    private ParticleSystem particulas;
    private AudioSource sonidoInterruptor;
    private LineRenderer linea;

    //Iniciar: Se ejecuta una única vez al ejecutar el programa
    void Start() {

        //Asignamos los valores de las referencias a otros componentes del objeto
        sprite = gameObject.GetComponent<SpriteRenderer>();
        particulas = gameObject.GetComponent<ParticleSystem>();
        sonidoInterruptor = gameObject.GetComponent<AudioSource>();
        linea = gameObject.GetComponentInParent<LineRenderer>();

    }

    //Al ingresar en Trigger 2D: Se ejecuta cuando algo entra en contacto con este objeto
    void OnTriggerEnter2D(Collider2D contacto) {

        //Si colisionó con un jugador y aún no fue activado
        if (contacto.gameObject.tag == "Player" && !fueActivado) {

            //Registra el contacto por consola y cambia el color del interruptor a verde (Tanto en sprite como en partículas)
            //Adicionalmente cambia su estado a activo dentro del sistema
            Debug.Log("Interruptor activado");
            sprite.sprite = spriteActivacion;
            particulas.startColor = Color.green;
            fueActivado = true;
            linea.enabled = true;
            sonidoInterruptor.Play();

        }
    }

    //Retorna si el interruptor fue activado o no
    public bool estadoActivacion() {

        return fueActivado;
        
    }
}
