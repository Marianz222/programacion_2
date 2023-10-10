using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorSoltadorBombas : MonoBehaviour
{

    //Campos serializados para configurar el script
    [Header("Configuracion")]
    [SerializeField] private Transform puntoOrigen;
    [SerializeField] private GameObject[] listaPrefabs;
    [SerializeField][Range(2, 5)] private int intervaloTiempo;
    [SerializeField][Range(1, 8)] private int tiempoInicial;
    [SerializeField] private bool registrosDebug = false;

    //Variables para la generación de numero aleatorio
    private int minimoRandom = 1;
    private int maximoRandom = 3;
    private int delimitadorRandom = 2;

    //Iniciar: Se llama una vez antes de la primer actualización de frame
    void Start()
    {

        //Llama al bucle que usa InvokeRepeating para generar objetos constantemente
        bucleGeneracionBomba();

    }

    //Instancia un objeto de tipo "Bomba Circular", utilizando el prefab de la misma como raíz de clonación
    private void generarBomba() {
        
        //Genera un número aleatorio entre los valores minimo y máximo
        int numeroAleatorio = Random.Range(minimoRandom, maximoRandom);
        int indiceAleatorio = Random.Range(0, listaPrefabs.Length);

    //Si el numero generado es mayor al delimitador (66% de probabilidad en este caso)
    if (numeroAleatorio >= delimitadorRandom) {

            //Instancia el objeto en la posición especificada con rotación neutral
            Instantiate(listaPrefabs[indiceAleatorio], puntoOrigen.position, Quaternion.identity);
            if (registrosDebug) { Debug.Log("[INFO/DEBUG]: " + listaPrefabs[indiceAleatorio].gameObject.name + " fue generado"); };

        }

    }

    //Llama a la generación en bucle del método de generación de bombas
    private void bucleGeneracionBomba() {

        //Llama al método de generación cada cierto tiempo. Dicho tiempo y el periodo inicial son configurables
        InvokeRepeating(nameof(generarBomba), tiempoInicial, intervaloTiempo);

    }

    //Desactiva la invocación constante del método de generación de bombas
    private void cancelarGeneracion() {

        //Cancela el llamado constante a generar bomba
        CancelInvoke(nameof(generarBomba));

    }

    //Al visibilizarse: Se llama cuando el objeto entra en el alcance de la cámara
    void OnBecameVisible() {

        //Registra la entrada por consola y llama a la reanudación del bucle de generacion
        if (registrosDebug) { Debug.Log("El generador ha entrado en distancia de renderizando. Activando..."); };
        bucleGeneracionBomba();

    }

    //Al invisibilizarse: Se llama cuando el objeto sale del alcance de la cámara
    void OnBecameInvisible() {

        //Registra la salida por consola y llama a cancelar generacion
        if (registrosDebug) { Debug.Log("El generador ha salido de la distancia de renderizado. Desactivando..."); };
        cancelarGeneracion();

    }
}
