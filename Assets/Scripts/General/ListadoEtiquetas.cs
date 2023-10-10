using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListadoEtiquetas : MonoBehaviour
{

    //Campos serializados de componentes
    [Header("Etiquetas Personalizadas")]
    [SerializeField] private bool esJugador;
    [SerializeField] private bool esMetal;
    [SerializeField] private bool esObjetoSolido;
    [SerializeField] private bool esDestructivo;

    //Retorna verdadero o falso si el objeto al que el componente está enlazado es un jugador
    public bool etiquetadoJugador() {

        if (esJugador)
        {

            return true;

        }
        else {

            return false;

        }

    }

    //Retorna verdadero o falso si el objeto al que el componente está enlazado es metal
    public bool etiquetadoMetal()
    {

        if (esMetal)
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    //Retorna verdadero o falso si el objeto al que el componente está enlazado es un objeto sólido
    public bool etiquetadoObjetoSolido()
    {

        if (esObjetoSolido)
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    //Retorna verdadero o falso si el objeto al que el componente está enlazado es una fuente de daño
    public bool etiquetadoDestructivo()
    {

        if (esDestructivo)
        {

            return true;

        }
        else
        {

            return false;

        }

    }
}
