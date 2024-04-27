using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestorEnemigos : MonoBehaviour
{
    public List<Enemigo> listaEnemigos;
    private int indiceActual = 0;
    public AudioSource victoriaSound; // AudioSource para sonido de victoria

    void Start()
    {
        DesactivarTodosEnemigos();
        IniciarEnemigo();
    }

    void DesactivarTodosEnemigos()
    {
        foreach (var enemigo in listaEnemigos)
        {
            enemigo.gameObject.SetActive(false);
        }
    }

    public void IniciarEnemigo()
    {
        if (indiceActual < listaEnemigos.Count && !listaEnemigos[indiceActual].EstaDestruido())
        {
            listaEnemigos[indiceActual].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("¡Todos los enemigos eliminados!");
            ReproducirSonidoVictoria();
            StartCoroutine(ReiniciarEscenaConRetraso());
        }
    }

    public void SiguienteEnemigo()
    {
        if (indiceActual < listaEnemigos.Count && !listaEnemigos[indiceActual].EstaDestruido())
        {
            listaEnemigos[indiceActual].gameObject.SetActive(false);
            indiceActual++;
            IniciarEnemigo();
        }
    }

    public int ObtenerIndiceActual()
    {
        return indiceActual;
    }

    private void ReproducirSonidoVictoria()
    {
        if (victoriaSound != null)
        {
            victoriaSound.Play();
        }
    }

    private IEnumerator ReiniciarEscenaConRetraso()
    {
        yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
