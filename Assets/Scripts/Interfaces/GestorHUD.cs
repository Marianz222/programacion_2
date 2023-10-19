using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class GestorHUD : MonoBehaviour
{

    //Seccion de referencias a componentes
    [Header("Referencias a Componentes")]
    [SerializeField] private GameObject objetoAnimador;
    [SerializeField] private GameObject objetoIconoEstadoJugador;
    [SerializeField] private Sprite nuevaTexturaBase;
    [SerializeField] private GameObject[] prefabsBarraEnergia;
    [SerializeField] private Sprite[] texturasBarraEnergiaContenedor;
    [SerializeField] private Sprite[] texturasBarraEnergiaInterior;
    [SerializeField] private Sprite[] texturasBarraEnergiaFondo;
    [SerializeField] private GameObject[] objetosContenedoresBarraEnergia;
    [SerializeField] private GameObject objetoRadialVida;

    //Sección de referencias a objetos de configuracion (Scriptable Objects)
    [Header("Referencias a Configuraciones")]
    [SerializeField] private ConfiguracionJugador datosJugador;

    //Variables locales privadas
    private Image iconoEstadoJugador;
    private Image baseEstadoJugador;
    private int vidaActual;
    private int vidaMaxima;
    private int energiaActual;
    private int energiaMaxima;

    //Iniciar: Se ejecuta antes de la primera actualización de frame
    void Start() {

        //Obtiene las imagenes para modificar la base y el icono del componente de interfaz "Estado Jugador"
        iconoEstadoJugador = objetoIconoEstadoJugador.transform.Find("Icono").GetComponent<Image>();
        baseEstadoJugador = objetoIconoEstadoJugador.transform.Find("Base").GetComponent<Image>();

        //Configura las variables locales con la data del objeto de configuracion
        vidaActual = datosJugador.VidaMaxima;
        vidaMaxima = datosJugador.VidaMaxima;
        energiaActual = datosJugador.EnergiaMaxima;
        energiaMaxima = datosJugador.EnergiaMaxima;

        //Llama a la función limitar atributos
        limitarAtributos();

        //Crea y actualiza la barra de energía
        crearBarraEnergia();
        actualizarBarraEnergia();

    }

    //Cambiar Estado Animador: Alterna el estado del animador entre activo/inactivo.
    //Usado para activar el animador tras ingresar al Nivel 1
    public void cambiarEstadoAnimador(bool estado) {

        //Activa/desactiva el objeto dependiendo el parámetro suministrado, registra la acción por consola
        objetoAnimador.SetActive(estado);
        Debug.Log("[INFO/DEBUG]: Modificado el estado de " + objetoAnimador.gameObject.name + " a " + estado);

    }

    //Limitar Atributos: Modifica las variables de número si exceden cierto límite
    void limitarAtributos() {

        //Crea 2 variables que contienen los limites
        int limiteVida = 500;
        int limiteEnergia = 10;

        //Si la vida máxima es superior al limite establecido...
        if (vidaMaxima > limiteVida) {

            //Altera los valores
            vidaMaxima = limiteVida;
            vidaActual = limiteVida;

        }

        //Si la energía máxima es superior al limite establecido...
        if (energiaMaxima > limiteEnergia) {

            //Altera los valores
            energiaMaxima = limiteEnergia;
            energiaActual = limiteEnergia;

        }

    }

    //Cambiar Icono Estado Jugador: Encargado de modificar la imagen que contiene el elemento de interfaz de "Estado Jugador"
    public void cambiarIconoEstadoJugador(Sprite sprite) {

        //Establece el sprite suministrado al elemento de interfaz
        //iconoEstadoJugador.sprite = sprite;

        //Llama a cambiar base de estado (eliminar)
        cambiarBaseEstadoJugador();

    }

    //ELIMINAR MÉTODO
    private void cambiarBaseEstadoJugador() {

        baseEstadoJugador.sprite = nuevaTexturaBase;
        Debug.Log("[INFO/DEBUG]: Textura Modificada para: Icono Estado Jugador");

    }

    //Crear Barra Energía: Instancia los elementos visuales de todas las secciones de la barra energética: Borde, Fondo y Relleno
    private void crearBarraEnergia() {

        //Ciclo que crea el BORDE
        for (int i = 0; i < energiaMaxima; i++)
        {

            //Instancia un nuevo fragmento de interfaz y lo almacena en una variable de objeto de juego
            GameObject objetoNuevo = Instantiate(prefabsBarraEnergia[0]);

            //Fija el contenedor de los elementos de borde como el padre del objeto, además lo re-escala
            objetoNuevo.gameObject.transform.SetParent(objetosContenedoresBarraEnergia[0].transform);
            objetoNuevo.gameObject.transform.localScale = new Vector2(1, 1);

            //Registra por consola que el icono se cargó correctamente
            Debug.Log("Cargado el Icono [" + i + "] para el Borde de la Barra Energética");

        }

        /////////////////////////////////////////////////////////////////////////////

        //Ciclo que crea el INTERIOR
        for (int i = 0; i < energiaActual; i++)
        {

            //Instancia un nuevo fragmento de interfaz y lo almacena en una variable de objeto de juego
            GameObject objetoNuevo = Instantiate(prefabsBarraEnergia[1]);

            //Fija el contenedor de los elementos de relleno como el padre del objeto, además lo re-escala
            objetoNuevo.gameObject.transform.SetParent(objetosContenedoresBarraEnergia[1].transform);
            objetoNuevo.gameObject.transform.localScale = new Vector2(1, 1);

            //Registra por consola que el icono se cargó correctamente
            Debug.Log("Cargado el Icono [" + i + "] para el Interior de la Barra Energética");

        }

        /////////////////////////////////////////////////////////////////////////////
        
        //Ciclo que crea el FONDO
        for (int i = 0; i < energiaMaxima; i++)
        {

            //Instancia un nuevo fragmento de interfaz y lo almacena en una variable de objeto de juego
            GameObject objetoNuevo = Instantiate(prefabsBarraEnergia[2]);

            //Fija el contenedor de los elementos de fondo como el padre del objeto, además lo re-escala
            objetoNuevo.gameObject.transform.SetParent(objetosContenedoresBarraEnergia[2].transform);
            objetoNuevo.gameObject.transform.localScale = new Vector2(1, 1);

            //Registra por consola que el icono se cargó correctamente
            Debug.Log("Cargado el Icono [" + i + "] para el Interior de la Barra Energética");

        }

        /////////////////////////////////////////////////////////////////////////////

        //Llama a Actualizar para que la barra cambie su aspecto dependiendo su tamaño
        actualizarBarraEnergia();

    }

    //Actualizar Barra Energia: Modifica el diseño de los componentes de la barra, esto dependiendo la cantidad de energía que
    //tenga el jugador, su energía máxima y otros aspectos
    public void actualizarBarraEnergia() {

        //Guarda la cantidad de elementos de interfaz para cada grupo: Energia Actual (Interior) y Energia Maxima (Borde y Fondo)
        int cantidadEnergia = objetosContenedoresBarraEnergia[0].transform.childCount;
        int cantidadEnergiaMaxima = objetosContenedoresBarraEnergia[1].transform.childCount;

        //Si la energia máxima es mayor a 0...
        if (cantidadEnergiaMaxima > 0) {

            //Si la energía máxima es 1...
            if (cantidadEnergiaMaxima == 1) {

                //Almacena la imagen que posee el componente de interfaz, asigna la nueva textura almacenada en el arreglo de texturas
                //Configura el nuevo diseño del BORDE
                Image imagenBorde = objetosContenedoresBarraEnergia[0].transform.GetChild(0).GetComponent<Image>();
                imagenBorde.sprite = texturasBarraEnergiaContenedor[0];

                ///////////////////////////////////////////////////////////////////////////////////

                //Almacena la imagen que posee el componente de interfaz, asigna la nueva textura almacenada en el arreglo de texturas
                //Configura el nuevo diseño del FONDO
                Image imagenFondo = objetosContenedoresBarraEnergia[2].transform.GetChild(0).GetComponent<Image>();
                imagenFondo.sprite = texturasBarraEnergiaFondo[0];

            //Sino si la energía máxima es mayor a 2...
            } else if (cantidadEnergiaMaxima >= 2) {

                //Almacena el índice del primer y el último objeto, debido a que los cambios de textura solo se realizan en los extremos
                int indicePrimeraImagenBorde = 0;
                int indiceUltimaImagenBorde = objetosContenedoresBarraEnergia[0].transform.childCount - 1;

                //Almacena las imagenes del Borde Izquierdo y el Derecho
                Image imagenExtremoIzquierdoBorde = objetosContenedoresBarraEnergia[0].transform.GetChild(indicePrimeraImagenBorde).GetComponent<Image>();
                Image imagenExtremoDerechoBorde = objetosContenedoresBarraEnergia[0].transform.GetChild(indiceUltimaImagenBorde).GetComponent<Image>();

                //Configura el nuevo diseño del ambos BORDES
                imagenExtremoIzquierdoBorde.sprite = texturasBarraEnergiaContenedor[2];
                imagenExtremoDerechoBorde.sprite = texturasBarraEnergiaContenedor[3];

                ///////////////////////////////////////////////////////////////////////////////////

                //Almacena el índice del primer y el último objeto, debido a que los cambios de textura solo se realizan en los extremos
                int indicePrimeraImagenFondo = 0;
                int indiceUltimaImagenFondo = objetosContenedoresBarraEnergia[2].transform.childCount - 1;

                //Almacena las imagenes del Fondo Izquierdo y el Derecho
                Image imagenExtremoIzquierdoFondo = objetosContenedoresBarraEnergia[2].transform.GetChild(indicePrimeraImagenFondo).GetComponent<Image>();
                Image imagenExtremoDerechoFondo = objetosContenedoresBarraEnergia[2].transform.GetChild(indiceUltimaImagenFondo).GetComponent<Image>();

                //Configura el nuevo diseño del ambos FONDOS
                imagenExtremoIzquierdoFondo.sprite = texturasBarraEnergiaFondo[2];
                imagenExtremoDerechoFondo.sprite = texturasBarraEnergiaFondo[3];

                //Ciclo FOR que recorre los elementos de interfaz centrales para cambiar su textura
                for (int i = 1; i < indiceUltimaImagenBorde; i++)
                {

                    //Guarda las referencias de las imagenes a modificar
                    Image imagenObjetoCentralBorde = objetosContenedoresBarraEnergia[0].transform.GetChild(i).GetComponent<Image>();
                    Image imagenObjetoCentralFondo = objetosContenedoresBarraEnergia[2].transform.GetChild(i).GetComponent<Image>();

                    //Modifica la imagen, reemplazandola por el diseño del centro
                    imagenObjetoCentralBorde.sprite = texturasBarraEnergiaContenedor[1];
                    imagenObjetoCentralFondo.sprite = texturasBarraEnergiaFondo[1];

                }

            }

        }

        /////////////////////////////////////////////////////////////////////////////

        //Si la energía es mayor a 0...
        if (cantidadEnergia > 0) {

            //Si la energía es 1...
            if (cantidadEnergia == 1)
            {

                //Almacena la imagen que posee el componente de interfaz, asigna la nueva textura almacenada en el arreglo de texturas
                //Configura el nuevo diseño del RELLENO
                Image imagenObjetoSeleccionado = objetosContenedoresBarraEnergia[1].transform.GetChild(0).GetComponent<Image>();
                imagenObjetoSeleccionado.sprite = texturasBarraEnergiaInterior[0];

            }
            //Sino si la energía es mayor a 2...
            else if (cantidadEnergia >= 2)
            {

                //Almacena el índice del primer y el último objeto, debido a que los cambios de textura solo se realizan en los extremos
                int indicePrimeraImagen = 0;
                int indiceUltimaImagen = objetosContenedoresBarraEnergia[1].transform.childCount - 1;

                //Almacena las imagenes del Relleno Izquierdo y el Derecho
                Image imagenExtremoIzquierdo = objetosContenedoresBarraEnergia[1].transform.GetChild(indicePrimeraImagen).GetComponent<Image>();
                Image imagenExtremoDerecho = objetosContenedoresBarraEnergia[1].transform.GetChild(indiceUltimaImagen).GetComponent<Image>();

                //Configura el nuevo diseño del ambos RELLENOS
                imagenExtremoIzquierdo.sprite = texturasBarraEnergiaInterior[2];
                imagenExtremoDerecho.sprite = texturasBarraEnergiaInterior[3];

                //Ciclo FOR que recorre los elementos de interfaz centrales para cambiar su textura
                for (int i = 1; i < indiceUltimaImagen; i++) {

                    //Guarda la referencia a la imagen a modificar
                    Image imagenObjetoCentral = objetosContenedoresBarraEnergia[1].transform.GetChild(i).GetComponent<Image>();

                    //Modifica la imagen, reemplazandola por el diseño del centro
                    imagenObjetoCentral.sprite = texturasBarraEnergiaInterior[1];

                }

            }

        }

    }

    //Modificar Energía Actual: Este método recibe un texto como parámetro, con el cual determina qué acción realizar.
    //Adiciona/Resta un punto de energía al jugador
    public void modificarEnergiaActual(string accion) {

        //Si la acción es "añadir"...
        if (accion == "add") {

            //Suma uno de energía
            energiaActual++;

            //Almacena el nuevo elemento de interfaz que se añadirá para simbolizar el nuevo punto de energía
            //Se asigna su padre y se limita su escala
            GameObject objetoNuevo = Instantiate(prefabsBarraEnergia[1]);
            objetoNuevo.gameObject.transform.SetParent(objetosContenedoresBarraEnergia[1].transform);
            objetoNuevo.gameObject.transform.localScale = new Vector2(1, 1);

            //Regista por consola que se añadió un nuevo elemento de interfaz
            Debug.Log("[INFO/DEBUG]: Se ha añadido 1 punto de energia");

            //Si la acción es "eliminar"...
        } else if (accion == "remove") {

            //Resta uno de energía
            energiaActual--;

            //Guarda el índice del último objeto (El componente de interfaz que se eliminará)
            int indiceObjeto = objetosContenedoresBarraEnergia[1].transform.childCount - 1;

            //Se destruye el objeto asociado al índice antes mencionado, restando visualmente la energía
            Destroy(objetosContenedoresBarraEnergia[1].transform.GetChild(indiceObjeto).gameObject);

            //Registra por consola que se eliminó un elemento de interfaz
            Debug.Log("[INFO/DEBUG]: Se ha consumido 1 punto de energia");

        } else {

            //Se registra una advertencia por consola, puesto que la palabra de acción fue introducida incorrectamente
            Debug.LogWarning("[INFO/WARNING]: Acción de modificación introducida incorrectamente. No se modificará el valor");

        }

        //Llama a Actualizar para que la barra cambie su aspecto dependiendo su tamaño
        actualizarBarraEnergia();

    }

    //Retornar Energía: Devuelve la cantidad de energía que le queda al jugador
    //Este método será movido a jugador junto a las variables de vida y energía, en la proxima actualización
    public int retornarEnergia() {

        return energiaActual;

    }

    //Retornar Vida: Devuelve la cantidad de vida que le queda al jugador
    //Al igual que el anterior, este método será movido a jugador junto a las variables de vida y energía, en la proxima actualización
    public int retornarVida()
    {

        return vidaActual;

    }

    //Dañar: Recibe la cantidad de vida a quitar por parámetro. Reduce los puntos de vida del jugador
    public void dañar(int puntos) {

        //Variables flotantes para la cantidad de vida actual (obtenida del slider) y los puntos convertidos a valor válido para el mismo
        float cantidadVida = objetoRadialVida.GetComponent<Image>().fillAmount;
        float puntosConvertidos = (float)puntos / 100;

        //Si la vida es menor o igual a el daño recibido...
        if (cantidadVida <= (puntosConvertidos)) {

            //La vida cambia a 0 y el jugador muere
            objetoRadialVida.GetComponent<Image>().fillAmount = 0;
            vidaActual = 0;

        //Sino si la vida es mayor o igual al daño...
        } else if (cantidadVida >= (puntosConvertidos)) {

            //Se resta el daño a la vida actual
            objetoRadialVida.GetComponent<Image>().fillAmount -= puntosConvertidos;
            vidaActual -= puntos;

        }

    }

}
