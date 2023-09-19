using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderingController : MonoBehaviour
{

    [SerializeField] public Transform posicionInicial;
    [SerializeField] public Transform posicionFinal;

    private LineRenderer linea;
    private bool debeRenderizarse = true;

    // Start is called before the first frame update
    void Start()
    {
        linea = GetComponent<LineRenderer>();
        linea.positionCount = 2;
        linea.SetPosition(0, posicionInicial.position);
        linea.SetPosition(1, posicionFinal.position);
    }

    void Update() {
        
        if (debeRenderizarse)
        {
            Invoke("LimpiarLinea", 2.0f);
            debeRenderizarse = false;
        }
    }

    private void LimpiarLinea() {

        linea.enabled = false;

    }
}
