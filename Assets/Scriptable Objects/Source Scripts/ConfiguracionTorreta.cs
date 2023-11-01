using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Crea el asset y lo guarda con el nombre y dirección especificadas
[CreateAssetMenu(fileName = "New Turret Data", menuName = "Custom/Turret")]

public class ConfiguracionTorreta : ScriptableObject
{

    [SerializeField]
    [Tooltip("La cantidad de proyectiles que pueden existir al mismo tiempo. Aumentar el valor hará que puedan existir más al mismo tiempo")]
    private int limiteProyectiles;

    [SerializeField]
    [Tooltip("Tiempo en segundos que le tomará a la torreta para empezar a disparar al entrar al Nivel")]
    private int tiempoActivacionInicial;

    [SerializeField]
    [Tooltip("Intervalo en segundos entre disparos")]
    private int tiempoIntervaloDisparo;

    public int LimiteProyectiles { get => limiteProyectiles; set => limiteProyectiles = value; }
    public int TiempoActivacion { get => tiempoActivacionInicial; set => tiempoActivacionInicial = value; }
    public int IntervaloDisparo { get => tiempoIntervaloDisparo; set => tiempoIntervaloDisparo = value; }

}
