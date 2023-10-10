using UnityEngine;

public class PropiedadesItem : MonoBehaviour
{

    //Referencias serializadas a componentes
    [SerializeField] private ConfiguracionRecolectables configuracion;

    //Variables locales y referencias privadas
    private SpriteRenderer sprite;
    private AudioSource reproductorAudio;
    private int rareza;
    private string identificador;


    //Configurar Elemento: Asigna el valor del archivo de configuracion insertado a los atributos del objeto, el resultado
    //dependerá del archivo de configuración que se inserte desde el editor
    private void configurarElemento()
    {

        //Las variables asignadas en valor son: Sprite, Clip de Sonido, Rareza e Identificador
        this.sprite.sprite = configuracion.Sprite;
        this.reproductorAudio.clip = configuracion.Sonido;
        this.rareza = configuracion.Rareza;
        this.identificador = configuracion.Identificador;

    }

    //Iniciar: Se llama una sola vez antes de la primera actualización de frame
    void Start()
    {

        //Asigna los componentes obtenidos a sus variables
        sprite = GetComponent<SpriteRenderer>();
        reproductorAudio = GetComponent<AudioSource>();

        //Llama a configurar elemento
        configurarElemento();


    }

    //Retornar Rareza: Devuelve la rareza del item
    public int retornarRareza() {

        return rareza;

    }

    //Retornar Identificador: Devuelve el ID del item
    public string retornarIdentificador() {

        return identificador;

    }

}
