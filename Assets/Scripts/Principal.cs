using UnityEngine;

public class Principal : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float multiplicadorVelocidadCorrer = 2f;
    public float factorCambioEscala = 0.1f;
    public float maxFactorCambioEscala = 0.3f;
    public AudioClip sonidoCaminar;
    public AudioClip sonidoKonami;

    private Transform Camara;

    private int indiceTeclaActual = 0;
    private readonly KeyCode[] codigoKonami = {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A
    };

    private Vector3 escalaInicial;
    private bool aumentandoEscala = true;
    private float velocidadAnimacion = 0f;

    private AudioSource audioSource;

    void Start()
    {
        escalaInicial = transform.localScale;
        velocidadAnimacion = velocidadMovimiento * factorCambioEscala;
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No se encontró un componente AudioSource en el GameObject.");
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(codigoKonami[indiceTeclaActual]))
            {
                indiceTeclaActual++;

                if (indiceTeclaActual >= codigoKonami.Length)
                {
                    MostrarMensajeKonami();
                    ReproducirSonido(sonidoKonami);
                    indiceTeclaActual = 0;
                }
            }
            else
            {
                indiceTeclaActual = 0;
            }
        }
    }

    void FixedUpdate()
    {
        Mover();
    }

    void Mover()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical).normalized;

        float velocidadActual = velocidadMovimiento;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidadActual *= multiplicadorVelocidadCorrer;
            velocidadAnimacion = velocidadActual * maxFactorCambioEscala;
        }
        else
        {
            velocidadAnimacion = velocidadActual * factorCambioEscala;
        }

        transform.Translate(movimiento * velocidadActual * Time.fixedDeltaTime, Space.World);

        if (movimiento != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movimiento, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
            AnimarCaminata(velocidadActual);
        }
        else
        {
            DetenerAnimacionCaminata();
        }

        if (Camara != null)
        {
            Camara.position = new Vector3(transform.position.x, Camara.position.y, transform.position.z);
        }
    }

    void AnimarCaminata(float velocidad)
    {
        if (aumentandoEscala)
        {
            float nuevaEscalaY = Mathf.Clamp(transform.localScale.y + Time.fixedDeltaTime * velocidad * maxFactorCambioEscala * 2f, escalaInicial.y, escalaInicial.y + maxFactorCambioEscala);
            transform.localScale = new Vector3(escalaInicial.x, nuevaEscalaY, escalaInicial.z);
        }
        else
        {
            float nuevaEscalaY = Mathf.Clamp(transform.localScale.y - Time.fixedDeltaTime * velocidad * maxFactorCambioEscala * 2f, 0.5f, escalaInicial.y);
            transform.localScale = new Vector3(escalaInicial.x, nuevaEscalaY, escalaInicial.z);
        }

        if (transform.localScale.y >= escalaInicial.y + maxFactorCambioEscala)
        {
            aumentandoEscala = false;
        }
        else if (transform.localScale.y <= 0.5f)
        {
            aumentandoEscala = true;
        }
    }

    void DetenerAnimacionCaminata()
    {
        float cambioEscala = Time.fixedDeltaTime * velocidadAnimacion * 2f;
        float nuevaEscalaY = Mathf.Lerp(transform.localScale.y, 1f, cambioEscala);
        transform.localScale = new Vector3(escalaInicial.x, nuevaEscalaY, escalaInicial.z);
    }

    void MostrarMensajeKonami()
    {
        Debug.Log("Código Konami Activado");
    }

    void ReproducirSonido(AudioClip sonido)
    {
        if (audioSource != null && sonido != null)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                audioSource.PlayOneShot(sonido, 1.5f);
            }
            else
            {
                audioSource.PlayOneShot(sonido);
            }
        }
        else
        {
            Debug.LogWarning("AudioSource o AudioClip es nulo. No se puede reproducir el sonido.");
        }
    }
}
