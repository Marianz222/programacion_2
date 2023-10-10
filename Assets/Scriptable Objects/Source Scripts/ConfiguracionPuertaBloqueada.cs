using UnityEngine;

//Crea el asset y lo guarda con el nombre y dirección especificadas
[CreateAssetMenu(fileName = "New Locked Door", menuName = "Custom/Locked Door")]

public class ConfiguracionPuertaBloqueada : ScriptableObject
{

    //Variable de usos
    [SerializeField]
    [Tooltip("La cantidad de items del tipo especificado que se requerirán para desbloquear esta puerta")]
    private int usosItem;

    //Variable booleana de uso instantáneo
    [SerializeField]
    [Tooltip("Si la puerta instantáneamente se abrirá tras introducir los items o habrá tiempo de espera")]
    private bool desbloqueoInstantaneo;

    //Variable de tiempo de desbloqueo
    [SerializeField]
    [Range(0.0f, 5.0f)]
    [Tooltip("Depende del atributo de desbloqueo instantáneo. El tiempo que tarda la puerta en abrirse si dicha opción está activa")]
    private int tiempoDesbloqueo;

    //Métodos de recuperación y asignación de variables
    public int Usos { get => usosItem; set => usosItem = value; }
    public bool DesbloqueoInstantaneo { get => desbloqueoInstantaneo; set => desbloqueoInstantaneo = value; }
    public int TiempoDesbloqueo { get => tiempoDesbloqueo; set => tiempoDesbloqueo = value; }

}
