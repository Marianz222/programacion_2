using UnityEngine;

//Crea el asset y lo guarda con el nombre y dirección especificadas
[CreateAssetMenu(fileName = "New Rarity System", menuName = "Custom/Rarity System")]

public class ConfiguracionRarezas : ScriptableObject
{

    //Atributos
    [SerializeField]
    [Tooltip("Los colores que se usan como base para el renderizado de los efectos para los items")]
    private Color[] colores;

}
