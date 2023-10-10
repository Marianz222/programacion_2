using System.Collections.Generic;
using UnityEngine;

public class GestorInventario : MonoBehaviour
{

    //Única variable de Diccionario
    Dictionary<string, GameObject> inventario;

    //Iniciar: Se llama una vez antes de la primera actualización de frame
    void Start() {

        //Inicializa el diccionario y registra la acción por consola
        inventario = new Dictionary<string, GameObject>();
        Debug.Log("[INFO/DEBUG]: Inventario creado");
        
    }

    //Agregar Item: Añade el item suministrado al inventario, utilizando el ID pasada por parámetro. Registra la acción por consola
    public void AgregarItem(string id, GameObject objeto) {

        inventario.Add(id, objeto);
        Debug.Log("[INFO/DEBUG]: Item agregado: " + objeto.gameObject.name + " con la ID: " + id);

    }

    //Retirar Item: Retira el item asociado a la ID pasada por parámetro. Registra la acción por consola
    public void retirarItem(string id) {

        inventario.Remove(id);
        Debug.Log("[INFO/DEBUG]: Item asociado a la ID: " + id + " fue eliminado");

    }

    //Obtener Item: Retorna el item que se encuentra en el diccionario (registrado con la llave "id")
    public GameObject obtenerItem(string id) {

        //Si el id no es nulo...
        if (id == null)
        {
            //Registra el error por consola y retorna un objeto de juego nulo
            Debug.LogError("[INFO/DEBUG]: La clave del ítem es nula");
            return null;
        }

        //crea una variable de tipo gameObject para guardar el objeto a retornar
        GameObject objetoRecuperado;
        //Intenta recuperar el valor del objeto alojado en el espacio "id" del inventario. Si encuentra, lo almacena en la variable
        inventario.TryGetValue(id, out objetoRecuperado);
        //Retorna el objeto recuperado
        return objetoRecuperado;

    }

    //Hay Item: Retorna verdadero si el inventario contiene un item registrado con el id pasado por parámetro
    public bool hayItem(string id) {

        //Retorna true si el objeto asociado al id no es nulo
        return obtenerItem(id) != null;

    }

}
