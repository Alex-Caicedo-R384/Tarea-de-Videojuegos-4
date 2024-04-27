using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespiracionSuave : MonoBehaviour
{
    public float frecuenciaRespiracion = 0.5f;
    public float amplitudRespiracion = 0.1f; 

    private Vector3 escalaInicial;

    void Start()
    {
        escalaInicial = transform.localScale;
    }

    void Update()
    {
        float cambioEscala = amplitudRespiracion * Mathf.Sin(Time.time * frecuenciaRespiracion * 2 * Mathf.PI);
        float nuevaEscalaY = escalaInicial.y + cambioEscala;
        transform.localScale = new Vector3(escalaInicial.x, nuevaEscalaY, escalaInicial.z);
    }
}
