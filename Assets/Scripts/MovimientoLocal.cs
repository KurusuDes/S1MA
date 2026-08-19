using UnityEngine;

/// <summary>
/// SEMANA 1 — Este script es DELIBERADAMENTE INCORRECTO para multijugador.
///
/// Mueve y colorea el cubo SOLO en la instancia donde se ejecuta. No tiene ni
/// una línea de red. Sirve para ver con los propios ojos el problema que el
/// resto del curso resuelve: cada instancia del juego vive en su propio mundo.
///
/// Semana 3 → pasará a heredar de NetworkBehaviour y el movimiento viajará.
/// Semana 4 → el color se arreglará con NetworkVariable.
/// </summary>
public class MovimientoLocal : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;

    private void Start()
    {
        // Cada instancia sortea su propio color. Por eso el Jugador 1 y el
        // Jugador 2 verán colores distintos para el MISMO cubo.
        var render = GetComponent<Renderer>();
        if (render != null)
        {
            render.material.color = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.9f, 1f);
        }
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direccion = new Vector3(h, 0f, v);
        transform.Translate(direccion * (velocidad * Time.deltaTime), Space.World);
    }
}
