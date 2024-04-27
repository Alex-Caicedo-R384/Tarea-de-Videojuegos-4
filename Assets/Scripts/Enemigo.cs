using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int indiceEnLista;
    private bool destruido = false;
    public AudioClip sonidoColision;
    public Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public bool EstaDestruido()
    {
        return destruido;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            rend.material.color = Color.blue;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Principal") && !destruido)
        {
            Debug.Log("Colisión con jugador: " + this.name);
            if (sonidoColision != null)
            {
                AudioSource.PlayClipAtPoint(sonidoColision, transform.position);
            }

            GestorEnemigos gestor = FindObjectOfType<GestorEnemigos>();
            if (gestor != null && indiceEnLista == gestor.ObtenerIndiceActual())
            {
                gestor.SiguienteEnemigo();
            }

            destruido = true;
            Destroy(this.gameObject);
        }
    }
}
