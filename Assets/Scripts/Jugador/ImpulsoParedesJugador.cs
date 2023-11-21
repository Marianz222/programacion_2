using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulsoParedesJugador : MonoBehaviour
{

    [SerializeField] private float fuerzaImpulsoX = 20.0f;
    [SerializeField] private float fuerzaImpulsoY = 25.0f;

    private Rigidbody2D cuerpo;
    private MovimientoJugador scriptMovimiento;
    private ControladorJugador datosJugador;

    private bool haSaltadoDesdePared = false;  // Variable para rastrear si ya se ha aplicado un salto desde la pared

    void Start()
    {

        cuerpo = GetComponent<Rigidbody2D>();
        scriptMovimiento = GetComponent<MovimientoJugador>();
        datosJugador = GetComponent<ControladorJugador>();

    }

    void FixedUpdate()
    {

        if (scriptMovimiento.enContactoConLimite())
        {

            // Reducir la velocidad vertical
            Vector2 velocidadReducida = new Vector2(cuerpo.velocity.x, cuerpo.velocity.y * 0.4f);
            cuerpo.velocity = velocidadReducida;

            // Si el jugador está en contacto con un colisionador a su izquierda
            if (scriptMovimiento.estaTocandoLimiteHorizontal(Vector2.left)) {

                aplicarImpulso(Vector2.right);

            }
            // Sino si el jugador está en contacto con un colisionador a su derecha
            else if (scriptMovimiento.estaTocandoLimiteHorizontal(Vector2.right)) {

                aplicarImpulso(Vector2.left);

            }
            else {
            
                // Si no está en contacto con ninguna pared, reiniciar la variable de salto desde la pared
                haSaltadoDesdePared = false;

            }
        }

    }

    void Update() {

        Debug.Log("Movimiento Activo: " + scriptMovimiento.movimientoPermitido());

    }

    private void aplicarImpulso(Vector2 direccion) {

        // Si la tecla de habilidad principal fue presionada y no ha saltado desde la pared
        if (Input.GetKeyDown(datosJugador.retornarTeclaHabilidadPrincipal()) /*&& !haSaltadoDesdePared*/) {

            Debug.Log("No se impulsó y está intentandolo");

            // Añade la fuerza del impulso usando la dirección, sin modificar la fuerza vertical
            Vector2 fuerzaImpulso = new Vector2(fuerzaImpulsoX * direccion.x, fuerzaImpulsoY);
            cuerpo.AddForce(fuerzaImpulso, ForceMode2D.Impulse);
            scriptMovimiento.intercambiarMovimiento(false);
            haSaltadoDesdePared = true;  // Marcar que se ha realizado un salto desde la pared

        }

    }

}
