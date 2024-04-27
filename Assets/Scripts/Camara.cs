using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform objetivo;
    public Vector3 offset = new Vector3(0f, 2f, -5f);

    void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 posicionDeseada = objetivo.position + offset;
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, Time.deltaTime * 5f);

        transform.LookAt(objetivo.position);
    }
}
