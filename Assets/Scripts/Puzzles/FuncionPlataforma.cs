using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class FuncionPlataforma : MonoBehaviour
{
    //Campos serializados
    [Header("Configuración de Variables")]
    [SerializeField] private float velocidadMovimiento = 0.0f;
    [SerializeField] private Transform puntoReferencia;
    [SerializeField] private GameObject plataformaActiva;
    [SerializeField] private GameObject plataformaInactiva;
    [SerializeField] private OpcionesMovimiento tipoMovimiento;

    [Header("Configuraciones específicas")]
    [Tooltip("Modificar este campo únicamente si se usa ROTACIÓN como movimiento")]
    [SerializeField]
    private DireccionRotacion direccionRotacion;

    //Variables locales
    private bool movimientoEfectuado = false;
    private bool interruptorActivado = false;

    void Start() {

        if (plataformaActiva != null) {

            plataformaActiva.SetActive(false);

        }

    }

    //Actualizar: Método llamado constantemente
    void Update()
    {
        
        //Si el interruptor ha sido activado...
        if (interruptorActivado)
        {

            definirMovimiento();

        }
    }

    //Definir Movimiento: Método encargado de detectar qué opcion de movimiento seleccionó el usuario mediante el Inspector, en base a
    //esto se fija el modo de movimiento que se usará para luego ser ejecutado
    private void definirMovimiento() {

        if (tipoMovimiento == OpcionesMovimiento.Mover) {

            ejecutarMover();

        } else if (tipoMovimiento == OpcionesMovimiento.Rotar) {

            ejecutarRotar();

        } else {

            ejecutarEscalar();

        }

    }

    //Activar Movimiento: Cambia la bandera de interruptor activado a true
    public void activarMovimiento() {

        interruptorActivado = true;

    }


    //Intercambiar Visuales: Este método alterna la bandera de activación de los sprites para las plataformas.
    //Cuando se ejecuta, si la renderización estaba en true, pasará a false y viceversa, permite intercambiar rápido entre los 2 visuales
    public void intercambiarVisuales() {

        if (plataformaActiva == null || plataformaInactiva == null) {

            return;

        }

        plataformaActiva.SetActive(!plataformaActiva.activeSelf);
        plataformaInactiva.SetActive(!plataformaActiva.activeSelf);

    }

    //Ejecutar Mover: Método que ejecuta el modo de movimiento por defecto, el cual mueve la plataforma hasta llegar al punto de referencia
    private void ejecutarMover() {

        if (movimientoEfectuado) {

            return;

        }

        //Mueve la plataforma en dirección al punto de control (visualizable en el editor)
        transform.position = Vector2.Lerp(transform.position, puntoReferencia.position, velocidadMovimiento * Time.deltaTime);

        //Si la distancia entre ambos cuerpos es muy baja...
        if (Vector2.Distance(transform.position, puntoReferencia.position) == 0)
        {

            //Se marca como movimiento efectuado para evitar futuros llamados
            movimientoEfectuado = true;

        }

    }

    //Ejecutar Rotar: Método que ejecuta el modo de movimiento de Rotación, el cual rota la plataforma usando el punto de referencia
    private void ejecutarRotar() {

        if (movimientoEfectuado)
        {

            return;

        }

        Vector3 direccion = new Vector3(0.0f, 0.0f);

        if (direccionRotacion == DireccionRotacion.Izquierda) {

            direccion.z = 1.0f;

        } else {

            direccion.z = -1.0f;

        }

        float coordenadaZ = this.gameObject.transform.rotation.z;

        if (coordenadaZ > -90 || coordenadaZ < 90) {

            transform.RotateAround(puntoReferencia.position, direccion, velocidadMovimiento * Time.deltaTime);

        }

    }

    //Ejecutar Escalar: Método que ejecuta el modo de movimiento de Escalado, el cual cambia el tamaño de la plataforma
    private void ejecutarEscalar() {

        //Placeholder

    }

    //Opciones Movimiento [Enumeración]: Contiene valores para definir los modos de movimiento de la plataforma
    enum OpcionesMovimiento {

        Mover,
        Rotar,
        Escalar

    }

    //Direccion Rotacion [Enumeración]: Contiene los valores para definir la dirección de rotación de la plataforma
    enum DireccionRotacion {

        Izquierda,
        Derecha

    }

}

