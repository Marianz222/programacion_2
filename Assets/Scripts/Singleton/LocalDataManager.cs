using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDataManager : MonoBehaviour
{

    public static LocalDataManager Instancia { get; private set; }

    void Awake()
    {

        if (Instancia == null)
        {

            Instancia = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {

            Destroy(gameObject);

        }

    }

    public void almacenarDatoEntero(string clave, int valor) {

        PlayerPrefs.SetInt(clave, valor);

    }

    public int cargarDatoEntero(string clave, int valorDefecto = 0) {

        return PlayerPrefs.GetInt(clave, valorDefecto);

    }

    public void guardarDatos() {

        PlayerPrefs.Save();

    }

    public void borrarDatos() {

        PlayerPrefs.DeleteAll();

    }

    public void borrarDatoAlojado(string clave) {

        PlayerPrefs.DeleteKey(clave);

    }

}
