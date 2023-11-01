using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Crea el asset y lo guarda con el nombre y dirección especificadas
[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Custom/Projectile")]

public class ConfiguracionProyectil : ScriptableObject
{

    [SerializeField]
    [Tooltip("El valor numérico de daño causado por el proyectil al impactar")]
    private int daño;

    [SerializeField]
    [Tooltip("La velocidad de movimiento del proyectil, limitado a 10 unidades")]
    [Range(1, 10)]
    private int velocidadMovimiento;

    [SerializeField]
    [Tooltip("El tiempo que le tomará al proyectil empezar a reaccionar a colisiones")]
    private float tiempoInmunidad;

    [SerializeField]
    [Tooltip("La cantidad de particulas que se emitirán cuando el proyectil colisione contra algun objeto")]
    private int numeroParticulas;
    public int Daño { get => daño; set => daño = value; }
    public int Velocidad { get => velocidadMovimiento; set => velocidadMovimiento = value; }
    public float TiempoInmunidad { get => tiempoInmunidad; set => tiempoInmunidad = value; }
    public int NumeroParticulas { get => numeroParticulas; set => numeroParticulas = value; }

}
