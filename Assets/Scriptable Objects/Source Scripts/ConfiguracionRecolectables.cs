using UnityEngine;

//Crea el asset y lo guarda con el nombre y dirección especificadas
[CreateAssetMenu(fileName = "New Collectable", menuName = "Custom/Collectable")]

public class ConfiguracionRecolectables : ScriptableObject
{

    //Variable de Sprite
    [SerializeField]
    [Tooltip("El sprite que el item tendrá")]
    private Sprite sprite;

    //Variable de Clip de Audio
    [SerializeField]
    [Tooltip("El sonido que el item tendrá al recolectarse")]
    private AudioClip sonido;

    //Identificador (ID)
    [SerializeField]
    [Tooltip("El identificador es la cadena de texto que define el tipo de item")]
    private string identificador;

    //Variable de Rareza (Rango)
    [SerializeField]
    [Range(1, 3)]
    [Tooltip("La rareza es un numero que define de qué color serán las partículas y efectos generados en el item")]
    private int rareza;


    //Métodos de recuperación y asignación de variables
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public AudioClip Sonido { get => sonido; set => sonido = value; }
    public string Identificador { get => identificador; set => identificador = value; }
    public int Rareza { get => rareza; set => rareza = value; }

}
