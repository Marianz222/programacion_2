using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class FuncionBotonFinal : MonoBehaviour
{

    [SerializeField] public Sprite spriteActivacion;
    [SerializeField] public GameObject jugador;

    private bool fueActivado = false;
    private SpriteRenderer sprite;
    private Mover scriptMovimiento;
    private Saltar scriptSalto;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        scriptMovimiento = jugador.gameObject.GetComponentInChildren<Mover>();
        scriptSalto = jugador.gameObject.GetComponentInChildren<Saltar>();

    }

    void OnTriggerEnter2D(Collider2D contacto) {

    if (contacto.gameObject.tag == "Player" && !fueActivado) {

            sprite.sprite = spriteActivacion;
            Debug.Log("Llegaste al Boton. Ganaste!");
            //jugador.SetActive(false);
            scriptMovimiento.enabled = false;
            scriptSalto.enabled = false;
            fueActivado = true;

    }

    }
}
