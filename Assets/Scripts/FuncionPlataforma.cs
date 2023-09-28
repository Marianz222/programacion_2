using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FuncionPlataforma : MonoBehaviour
{
    //Campos serializados
    [Header("Configuración de Variables")]
    [SerializeField] public float velocidadMovimiento = 0.0f;
    [SerializeField] public Transform posicionFinal;
    [SerializeField] public GameObject interruptor;

    //Variables locales
    private bool movimientoEfectuado = false;
    private FuncionInterruptor scriptInterruptor;

    //Inicio: Método llamado una única vez al comienzo de la ejecución
    void Start()
    {
        //Se obtiene el componente de Script "FuncionInterruptor" y se almacena
        scriptInterruptor = interruptor.GetComponent<FuncionInterruptor>();
    }

    //Actualizar: Método llamado constantemente
    void Update()
    {
        //Si el interruptor ha sido activado y aún no se efectuó el movimiento...
        if (scriptInterruptor.estadoActivacion() && !movimientoEfectuado)
        {
            //Mueve la plataforma en dirección al punto de control (visualizable en el editor)
            transform.position = Vector2.MoveTowards(transform.position, posicionFinal.position, velocidadMovimiento * Time.deltaTime);

            //Si la distancia entre ambos cuerpos es muy baja...
            if (Vector2.Distance(transform.position, posicionFinal.position) < 0)
            {
                //Se marca como movimiento efectuado para evitar futuros llamados
                movimientoEfectuado = true;
            }
        }
    }
}

