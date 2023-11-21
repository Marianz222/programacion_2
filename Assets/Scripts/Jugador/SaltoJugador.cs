using UnityEngine;

public class SaltoJugador : MonoBehaviour
{

    [SerializeField] private LayerMask capaObjetivo;

    //Variables privadas, solo accesibles desde el comportamiento actual
    private bool puedeSaltar = true;
    private bool estaSaltando = false;
    private float distanciaRaycast = 0.5f;

    //Referencia al Cuerpo Rígido localizado en el mismo Objeto de Juego
    private Rigidbody2D cuerpo;
    private Animator animador;
    private AudioSource sonidoColision;
    private AudioSource sonidoSalto;
    private ParticleSystem sistemaParticulas;
    private ControladorJugador datosJugador;
    private MovimientoJugador scriptMovimiento;

    //Variables locales y privadas
    private float fuerzaSalto;

    //Al Activarse: Se ejecuta una vez, antes de la primer actualización
    private void Start()
    {
        //Llama a obtener componentes
        obtenerComponentes();

        //Asigna el valor de altura de salto del jugador a una variable local para facilitar su uso
        fuerzaSalto = datosJugador.retornarAlturaSalto();

    }

    //Obtener Componentes: Recupera el valor de los componentes que usará el script y los guarda en variables
    private void obtenerComponentes()
    {
        
        cuerpo = GetComponent<Rigidbody2D>();
        animador = GetComponentInChildren<Animator>();
        sonidoColision = GetComponentInParent<AudioSource>();
        sonidoSalto = GetComponent<AudioSource>();
        sistemaParticulas = GetComponentInChildren<ParticleSystem>();
        datosJugador = GetComponent<ControladorJugador>();
        scriptMovimiento = GetComponent<MovimientoJugador>();

    }

    //Actualización: Se ejecuta constantemente, pero el intervalo puede variar
    void Update()
    {

        puedeSaltar = estaTocandoSuelo();

        //Si el jugador está pulsando "Espacio" y puede saltar...
        if (Input.GetKeyDown(datosJugador.retornarTeclaHabilidadPrincipal()) && puedeSaltar) {

            saltar();

            //Llama a la función de salto
            efectosSalto();

        }

    }

    //Saltar: Se ejecuta una única vez, y se encarga de manipular el cuerpo rígido (añadiendo el impulso vertical), además de gestionar
    //las variables booleanas usadas en el proceso y condicionar el salto tras ciertas condiciones
    void saltar() {

        //Impulsa al cuerpo hacia arriba, usando la fuerza de salto en multiplicación con el valor actual de x. Está saltando pasa a ser "true"
        cuerpo.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

        estaSaltando = true;

    }

    void efectosSalto() {

        animador.Play("Main.Burst", 0, 0);
        sonidoSalto.Play();
        sistemaParticulas.Emit(5);

    }

    private bool estaTocandoSuelo() {

        RaycastHit2D rayo = Physics2D.Raycast(transform.position, Vector2.down, distanciaRaycast, capaObjetivo);

        if (rayo.collider != null && !rayo.collider.gameObject.CompareTag("Player")) {

            estaSaltando = false;

            scriptMovimiento.intercambiarMovimiento(true);

            return true;

        }

        return false;

    }

    private void OnDrawGizmos() {

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector2.down * distanciaRaycast);

    }

}