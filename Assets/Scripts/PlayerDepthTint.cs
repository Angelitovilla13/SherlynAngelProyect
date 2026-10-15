using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerDepthTint : MonoBehaviour
{
    [SerializeField] private PlayerDepthScale depthScale;
    [SerializeField] private Color farColor = new Color(0.55f, 0.6f, 0.7f);  // lejos: oscuro y frío
    [SerializeField] private Color nearColor = Color.white;                    // cerca: normal

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        sr.color = Color.Lerp(farColor, nearColor, depthScale.DepthRatio);
    }
}