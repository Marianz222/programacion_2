using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{

    [SerializeField] private LayerMask capaObjetivo;

    //Variables privadas, solo accesibles desde el comportamiento actual
    private float movimiento;
    private Vector2 direccion;

    //Referencia al Cuerpo Rígido localizado en el mismo Objeto de Juego
    private Rigidbody2D cuerpo;
    private ControladorJugador datosJugador;
    private MovimientoJugador scriptMovimiento;

    private float velocidad;
    private float distanciaRaycast = 0.45f;
    private bool puedeDesplazarseIzquierda = true;
    private bool puedeDesplazarseDerecha = true;
    private bool movimientoActivado = true;

    //Al Activarse: Se ejecuta una vez, antes de la primer actualización
    void Start()
    {

        //Se obtiene el componente de cuerpo rígido, para almacenarlo en su variable y usarse
        cuerpo = GetComponent<Rigidbody2D>();
        datosJugador = GetComponent<ControladorJugador>();
        scriptMovimiento = GetComponent<MovimientoJugador>();

        velocidad = datosJugador.retornarVelocidadMovimiento();

    }

    //Actualización: Se ejecuta constantemente, pero el intervalo puede variar
    void Update()
    {

        if (!movimientoActivado) {

            return;

        }

        puedeDesplazarseIzquierda = !estaTocandoLimiteHorizontal(Vector2.left);
        puedeDesplazarseDerecha = !estaTocandoLimiteHorizontal(Vector2.right);

        movimiento = Input.GetAxis("Horizontal");

        direccion = new Vector2(movimiento, 0f);

    }

    //Actualización Fija: Se ejecuta constantemente, pero su intervalo nunca varía. Se usa para el cálculo de colisiones
    void FixedUpdate()
    {

        // Verifica que el objeto esté activo antes de intentar moverlo
        if (gameObject.activeSelf && movimientoActivado)
        {
            // Desplaza al jugador solo si puede desplazarse en esa dirección
            if (puedeDesplazarseIzquierda && movimiento < -0.1f)
            {
                // Desplazarse a la izquierda
                Vector2 velocidadNueva = new Vector2(movimiento * velocidad, cuerpo.velocity.y);
                cuerpo.velocity = velocidadNueva;

            }
            else if (puedeDesplazarseDerecha && movimiento > 0.1f)
            {

                // Desplazarse a la derecha
                Vector2 velocidadNueva = new Vector2(movimiento * velocidad, cuerpo.velocity.y);
                cuerpo.velocity = velocidadNueva;

            }
            else
            {

                // No puede desplazarse en esa dirección, por lo que la velocidad en x es 0
                Vector2 velocidadNueva = new Vector2(0f, cuerpo.velocity.y);
                cuerpo.velocity = velocidadNueva;

            }
            
        }

    }

    public bool estaTocandoLimiteHorizontal(Vector2 direccion) {

        RaycastHit2D rayoCentral = Physics2D.Raycast(transform.position, direccion, distanciaRaycast, capaObjetivo);

        return rayoCentral.collider != null;

    }

    public bool enContactoConLimite() {

        RaycastHit2D rayoIzquierdo = Physics2D.Raycast(transform.position, Vector2.left, distanciaRaycast, capaObjetivo);
        RaycastHit2D rayoDerecho = Physics2D.Raycast(transform.position, Vector2.right, distanciaRaycast, capaObjetivo);

        scriptMovimiento.intercambiarMovimiento(true);

        return rayoIzquierdo.collider != null || rayoDerecho.collider != null;

    }

    private void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.left * distanciaRaycast);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector2.right * distanciaRaycast);

    }

    public void intercambiarMovimiento(bool estado) {

        movimientoActivado = estado;

    }

    public bool movimientoPermitido() {

        return movimientoActivado;

    }

}