using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarCamara : MonoBehaviour
{
    public Camera camaraPrincipal;
    public Camera camaraJugador;

    void Start()
    {
        camaraJugador.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            camaraPrincipal.enabled = !camaraPrincipal.enabled;
            camaraJugador.enabled = !camaraJugador.enabled;
        }
    }
}
