using UnityEngine;

public class SaltoPersonaje : MonoBehaviour
{

    //Se crea el título de la sección de campos serializados, además de añadir los mismos
    [Header("Configuracion de Variables")]
    [SerializeField] private float fuerzaSalto = 5.0f;

    //Variables privadas, solo accesibles desde el comportamiento actual
    private bool puedeSaltar = true;
    private bool estaSaltando = false;

    //Referencia al Cuerpo Rígido localizado en el mismo Objeto de Juego
    private Rigidbody2D cuerpo;
    private Animator animador;
    private AudioSource sonidoColision;
    private AudioSource sonidoSalto;
    private ParticleSystem sistemaParticulas;

    //Al Activarse: Se ejecuta una vez, antes de la primer actualización
    private void Start()
    {
        //Se obtiene el componente de cuerpo rígido, para almacenarlo en su variable y usarse
        cuerpo = GetComponent<Rigidbody2D>();
        animador = GetComponentInChildren<Animator>();
        sonidoColision = GetComponentInParent<AudioSource>();
        sonidoSalto = GetComponent<AudioSource>();
        sistemaParticulas = GetComponentInChildren<ParticleSystem>();
    }

    //Actualización: Se ejecuta constantemente, pero el intervalo puede variar
    void Update()
    {
        //Si el jugador está pulsando "Espacio" y puede saltar...
        if (Input.GetKeyDown(KeyCode.Space) && puedeSaltar)
        {
            //Llama a la función de salto
            saltar();

        }
    }

    //Saltar: Se ejecuta una única vez, y se encarga de manipular el cuerpo rígido (añadiendo el impulso vertical), además de gestionar
    //las variables booleanas usadas en el proceso y condicionar el salto tras ciertas condiciones
    void saltar() {

        //Si no puede saltar y no está saltando...
        if (puedeSaltar && !estaSaltando)
        {
            //Impulsa al cuerpo hacia arriba, usando la fuerza de salto en multiplicación con el valor actual de x. Está saltando pasa a ser "true"
            cuerpo.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            estaSaltando = true;
            puedeSaltar = false;
            animador.Play("Main.Burst", 0, 0);
            sonidoSalto.Play();
            sistemaParticulas.Emit(5);
        }

    }

    //Al recibir Colision 2D: Se ejecuta cuando el cuerpo colisiona con otros en la escena
    private void OnCollisionEnter2D(Collision2D contacto)
    {
        //Resetea ambas variables de estado, colocandolas en "false"
        puedeSaltar = true;
        estaSaltando = false;

        //Si el objeto con el que se colisionó está identificado como Metal
        if (contacto.gameObject.tag == "Metal") {

            //Reproducir sonido: Colisión en Metal (Desactivado)
            //sonidoColision.Play();

        }
    }

}