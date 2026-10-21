using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    [Header("Referencias del Borde")]
    public LineRenderer lineRenderer;

    [Header("Vista Interior del Cofre")]
    public GameObject insideViewCanvas; // <--- AQUÍ está la casilla para la imagen
    public Transform player;             // <--- AQUÍ está la casilla para el Player

    private bool cercaDelCofre = false;

    private void Start()
    {
        // Genera el borde automáticamente al iniciar el juego
        ActualizarBorde();
        if (insideViewCanvas != null) insideViewCanvas.SetActive(false);
    }

    public void ActualizarBorde()
    {
        PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
        if (poly != null && lineRenderer != null)
        {
            Vector2[] puntos = poly.points;
            lineRenderer.useWorldSpace = false;
            lineRenderer.positionCount = puntos.Length + 1;

            for (int i = 0; i < puntos.Length; i++)
            {
                lineRenderer.SetPosition(i, puntos[i] + poly.offset);
            }
            lineRenderer.SetPosition(puntos.Length, puntos[0] + poly.offset);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cercaDelCofre = true;
            if (lineRenderer != null) lineRenderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cercaDelCofre = false;
            if (lineRenderer != null) lineRenderer.enabled = false;
            if (insideViewCanvas != null) insideViewCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (cercaDelCofre && Input.GetKeyDown(KeyCode.E))
        {
            if (insideViewCanvas != null)
            {
                // Abre o cierra la imagen del cofre al presionar la E
                insideViewCanvas.SetActive(!insideViewCanvas.activeSelf);
            }
        }
    }
}