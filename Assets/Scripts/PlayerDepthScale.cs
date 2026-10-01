using UnityEngine;

public class PlayerDepthScale : MonoBehaviour
{
    [Header("Referencias de profundidad")]
    [SerializeField] private Transform farMarker;
    [SerializeField] private Transform nearMarker;

    [Header("Escala")]
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 1f;

    // Qué tan "cerca" está el jugador, de 0 (lejos) a 1 (cerca).
    // PlayerMovement lo usa para frenar el movimiento horizontal al fondo.
    public float DepthRatio { get; private set; } = 1f;

    private void Update()
    {
        float t = Mathf.InverseLerp(farMarker.position.y, nearMarker.position.y, transform.position.y);
        t = Mathf.Clamp01(t);
        DepthRatio = t;

        float scale = Mathf.Lerp(minScale, maxScale, t);
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}