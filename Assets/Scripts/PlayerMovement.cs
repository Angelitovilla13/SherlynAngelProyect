using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Velocidad")]
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float verticalSpeed = 2f;

    [Header("Profundidad")]
    [SerializeField] private PlayerDepthScale depthScale;
    [Range(0f, 1f)]
    [SerializeField] private float minHorizontalMultiplier = 0.3f;
    [Range(0f, 1f)]
    [SerializeField] private float minVerticalMultiplier = 0.25f; // qué tan lento sube al fondo

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    private Vector2 moveInput;
    private bool isFrozen = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isFrozen)
        {
            moveInput = Vector2.zero;
            UpdateAnimator();
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(horizontal, vertical);

        if (horizontal > 0)
            sr.flipX = false;
        else if (horizontal < 0)
            sr.flipX = true;

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        animator.SetBool("IsMoving", isMoving);
    }

    private void FixedUpdate()
    {
        float depthRatio = depthScale.DepthRatio; // 0 = lejos, 1 = cerca

        float horizontalMultiplier = Mathf.Lerp(minHorizontalMultiplier, 1f, depthRatio);
        float verticalMultiplier = Mathf.Lerp(minVerticalMultiplier, 1f, depthRatio);

        Vector2 scaledInput = new Vector2(
            moveInput.x * horizontalSpeed * horizontalMultiplier,
            moveInput.y * verticalSpeed * verticalMultiplier
        );

        rb.MovePosition(rb.position + scaledInput * Time.fixedDeltaTime);
    }

    public void FreezeMovement() => isFrozen = true;
    public void UnfreezeMovement() => isFrozen = false;
}