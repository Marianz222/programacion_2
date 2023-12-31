using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Crea el asset y lo guarda con el nombre y dirección especificadas
[CreateAssetMenu(fileName = "New Player Data", menuName = "Custom/Player")]

public class ConfiguracionJugador : ScriptableObject
{

    //Variables de tipo "Sprite"
    [SerializeField]
    [Tooltip("El sprite que se usará para representar al estado de Cubo")]
    private Sprite spriteEstadoCubo;

    [SerializeField]
    [Tooltip("El sprite que se usará para representar al estado de Sierra")]    
    private Sprite spriteEstadoSierra;

    [SerializeField]
    [Tooltip("El sprite que se usará para representar al estado de Araña")]
    private Sprite spriteEstadoAracnido;

    //Variables de tipo "Clip de Audio"
    [SerializeField]
    [Tooltip("El sonido que se reproducirá al saltar")]
    private AudioClip sonidoCuboPropulsor;

    [SerializeField]
    [Tooltip("El sonido que se reproducirá al cargar la Sierra")]
    private AudioClip sonidoSierraCarga;


    //Variables de tipo "Tecla"
    [SerializeField]
    [Tooltip("La tecla que permite cambiar al Estado de Cubo")]
    private KeyCode teclaEstadoCubo;

    [SerializeField]
    [Tooltip("La tecla que permite cambiar al Estado de Sierra")]
    private KeyCode teclaEstadoSierra;

    [SerializeField]
    [Tooltip("La tecla que permite cambiar al Estado de Araña")]
    private KeyCode teclaEstadoAracnido;

    [SerializeField]
    [Tooltip("La tecla que permite activar la habilidad principal")]
    private KeyCode teclaHabilidadPrincipal;

    [SerializeField]
    [Tooltip("La tecla que permite activar la habilidad secundaria")]
    private KeyCode teclaHabilidadSecundaria;


    //Variables primitivas generales
    [SerializeField]
    [Tooltip("La vida máxima que puede llegar a tener el jugador")]
    private int vidaMaxima;

    [SerializeField]
    [Tooltip("La energia máxima que puede llegar a tener el jugador")]
    private int energiaMaxima;

    [SerializeField]
    [Tooltip("La velocidad de movimiento horizontal del jugador")]
    private float velocidadMovimiento;

    [SerializeField]
    [Tooltip("La altura a la que salta el jugador (Estado Cubo)")]
    private float alturaSalto;

    [SerializeField]
    [Tooltip("Si es posible permanecer saltando al mantener presionada la tecla de salto")]
    private bool permitirSaltoContinuo;

    [SerializeField]
    [Tooltip("Si es posible saltar tras colisionar sobre una superficie a los lados")]
    private bool permitirSaltoParedes;

    [SerializeField]
    [Tooltip("Controla si se muestran o no las partículas asociadas al Jugador")]
    private bool tieneParticulas;


    //Métodos de recuperación y asignación de variables para "Sprite"
    public Sprite SpriteCubo { get => spriteEstadoCubo; set => spriteEstadoCubo = value; }
    public Sprite SpriteSierra { get => spriteEstadoSierra; set => spriteEstadoSierra = value; }
    public Sprite SpriteAracnido { get => spriteEstadoAracnido; set => spriteEstadoAracnido = value; }

    //Métodos de recuperación y asignación de variables para "Clip de Audio"
    public AudioClip SonidoCuboPropulsor { get => sonidoCuboPropulsor; set => sonidoCuboPropulsor = value; }
    public AudioClip SonidoSierraCarga { get => sonidoSierraCarga; set => sonidoSierraCarga = value; }

    //Métodos de recuperación y asignación de variables para "Código de Tecla"
    public KeyCode TeclaEstadoCubo { get => teclaEstadoCubo; set => teclaEstadoCubo = value; }
    public KeyCode TeclaEstadoSierra { get => teclaEstadoSierra; set => teclaEstadoSierra = value; }
    public KeyCode TeclaEstadoAracnido { get => teclaEstadoAracnido; set => teclaEstadoAracnido = value; }
    public KeyCode TeclaHabilidadPrincipal { get => teclaHabilidadPrincipal; set => teclaHabilidadPrincipal = value; }
    public KeyCode TeclaHabilidadSecundaria { get => teclaHabilidadSecundaria; set => teclaHabilidadSecundaria = value; }

    //Métodos de recuperación y asignación de variables básicas
    public int VidaMaxima { get => vidaMaxima; set => vidaMaxima = value; }
    public int EnergiaMaxima { get => energiaMaxima; set => energiaMaxima = value; }
    public float VelocidadMovimiento { get => velocidadMovimiento; set => velocidadMovimiento = value; }
    public float AlturaSalto { get => alturaSalto; set => alturaSalto = value; }

}
