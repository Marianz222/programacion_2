using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal.Internal;

public class FuncionInterruptor : MonoBehaviour
{

    //Campos serializados para componentes
    [Header("Variables a Configurar")]
    [SerializeField] private Sprite spriteInactivo;
    [SerializeField] private Sprite spriteActivo;
    [SerializeField] private Color colorInactivo;
    [SerializeField] private Color colorActivo;

    [Header("Eventos Disponibles")]
    [SerializeField] private UnityEvent AlActivarInterruptor;

    //Variables locales
    private bool fueActivado = false;

    //Referencias a componentes
    private SpriteRenderer sprite;
    private ParticleSystem particulas;
    private AudioSource sonidoInterruptor;
    private LineRenderer linea;

    //Iniciar: Se ejecuta una única vez al ejecutar el programa
    void Start()
    {

        //Llamada a obtener componentes
        obtenerComponentes();

        //Llamada a configuración inicial
        configuracionInicial();

    }

    //Configuración Inicial: Se llama en start y configura un valor por defecto para ciertas variables y componentes
    private void configuracionInicial() {

        //Si el componente de renderizador de sprites no es nulo...
        if (sprite != null) {

            //Por defecto, el sprite aplicado es el inactivo
            sprite.sprite = spriteInactivo;

        }

    }

    //Obtener Componentes: Guarda los componentes en variables para poder referenciarlos en el script
    private void obtenerComponentes()
    {

        //Asignamos los valores de las referencias a otros componentes del objeto
        gameObject.TryGetComponent<SpriteRenderer>(out sprite);

        particulas = gameObject.GetComponent<ParticleSystem>();
        sonidoInterruptor = gameObject.GetComponent<AudioSource>();
        linea = gameObject.GetComponentInParent<LineRenderer>();

    }

    //Al ingresar en Trigger 2D: Se ejecuta cuando algo entra en contacto con este objeto
    void OnTriggerEnter2D(Collider2D contacto)
    {

        //Si colisionó con un jugador y aún no fue activado
        if (contacto.gameObject.CompareTag("Player") && !fueActivado)
        {

            //Cambia el sprite a su versión activada y reproduce el sonido
            if (sprite != null) {

                sprite.sprite = spriteActivo;

            }

            if (sonidoInterruptor != null) {

                sonidoInterruptor.Play();

            }

            //Guarda el módulo principal del sistema de partículas en una variable para poder cambiar el color

            if (particulas != null) {

                var moduloPrincipal = particulas.main;
                moduloPrincipal.startColor = colorActivo;

            }

            //Se llama al evento de "Al activar interruptor"
            AlActivarInterruptor.Invoke();

            //Cambia el bool de activación a verdadero y registra por consola
            fueActivado = true;
            Debug.Log("Interruptor activado");

        }

    }

    //Colorear Partículas: Método que usa el color pasado por parámetro para aplicarlo al sistema de particulas
    //EL MÉTODO NO FUNCIONA Y NO SÉ POR QUE, QUEDA EN DESUSO
    private void colorearParticulas(Color color) {

        //Almacena una referencia al módulo principal del sistema de particulas y le cambia el color
        var moduloPrincipal = particulas.main;
        moduloPrincipal.startColor = color;

    }

}
