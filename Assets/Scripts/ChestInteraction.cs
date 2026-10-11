using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    [Header("Referencias visuales (cercanía)")]
    [SerializeField] private GameObject chestOutline;
    [SerializeField] private GameObject promptE;

    [Header("Vista interior")]
    [SerializeField] private GameObject insideViewCanvas;
    [SerializeField] private PlayerMovement player;

    private bool isPlayerNear = false;
    private bool isInsideViewOpen = false;

    private void Awake()
    {
        if (chestOutline != null) chestOutline.SetActive(false);
        if (promptE != null) promptE.SetActive(false);
        if (insideViewCanvas != null) insideViewCanvas.SetActive(false);
    }

    private void Update()
    {
        bool pressedE = Input.GetKeyDown(KeyCode.E);
        bool pressedEscape = Input.GetKeyDown(KeyCode.Escape);

        if (!isInsideViewOpen && isPlayerNear && pressedE)
        {
            OpenInsideView();
        }
        else if (isInsideViewOpen && (pressedE || pressedEscape))
        {
            CloseInsideView();
        }
    }

    private void OpenInsideView()
    {
        isInsideViewOpen = true;
        insideViewCanvas.SetActive(true);
        player.FreezeMovement();

        // Ocultamos el aviso de "Presiona E" mientras está abierto, para no confundir
        if (promptE != null) promptE.SetActive(false);
    }

    private void CloseInsideView()
    {
        isInsideViewOpen = false;
        insideViewCanvas.SetActive(false);
        player.UnfreezeMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = true;
        if (chestOutline != null) chestOutline.SetActive(true);
        if (promptE != null) promptE.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = false;
        if (chestOutline != null) chestOutline.SetActive(false);
        if (promptE != null) promptE.SetActive(false);
    }
}