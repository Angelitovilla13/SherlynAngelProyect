using UnityEngine;

public class InteractiveDoor : MonoBehaviour
{
    [Header("Destino")]
    [SerializeField] private string targetSceneName;
    [SerializeField] private string targetSpawnPointName;

    [Header("Referencias visuales")]
    [SerializeField] private GameObject doorGlow;
    [SerializeField] private GameObject promptE;

    private bool isPlayerNear = false;

    private void Awake()
    {
        if (doorGlow != null) doorGlow.SetActive(false);
        if (promptE != null) promptE.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            SceneTransitionManager.Instance.GoToScene(targetSceneName, targetSpawnPointName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = true;
        if (doorGlow != null) doorGlow.SetActive(true);
        if (promptE != null) promptE.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = false;
        if (doorGlow != null) doorGlow.SetActive(false);
        if (promptE != null) promptE.SetActive(false);
    }
}