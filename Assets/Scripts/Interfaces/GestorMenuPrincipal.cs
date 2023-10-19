using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GestorMenuPrincipal : MonoBehaviour
{

    //Sección de configuración de variables
    [Header("Configuracion de Variables")]
    [SerializeField] private int indiceInterfazMenu;

    //Sección de registro de eventos
    [Header("Eventos Disponibles")]
    [SerializeField] UnityEvent AlIngresarAlJuego;


    //Cambiar Escena: Recibe el índice de la escena objetivo como parámetro y fija esa escena como activa, desactivando el menú
    public void cambiarEscena(int indiceEscenaObjetivo) {

        //Si el índice objetivo es el mismo que el del menú...
        if (indiceEscenaObjetivo == indiceInterfazMenu) {

            //Registra el error en la consola y sale de la función
            Debug.LogError("[DEBUG/ERROR]: El indice de la escena objetivo y de la escena actual son iguales");
            return;

        }

        //Guarda las referencias a las escenas a intercambiar
        Scene siguienteEscena = SceneManager.GetSceneByBuildIndex(indiceEscenaObjetivo);
        Scene anteriorEscena = SceneManager.GetSceneByBuildIndex(indiceInterfazMenu);

        //Carga la escena objetivo, pasando el índice por parámetro
        //SceneManager.LoadScene(sceneBuildIndex: indiceEscenaObjetivo);

        //Activa la nueva escena y descarga la anterior
        SceneManager.SetActiveScene(siguienteEscena);
        SceneManager.UnloadSceneAsync(anteriorEscena);

        //Llama al evento de ingreso al juego y registra dicha acción por consola
        Debug.Log("[INFO/EVENTOS]: Evento 'Ingresar al Juego' ha sido llamado");
        AlIngresarAlJuego.Invoke();

    }

    //Cerrar Juego: Método encargado de cerrar la aplicación
    public void cerrarJuego() {

        //Cierra el juego
        Application.Quit();

    }

}
