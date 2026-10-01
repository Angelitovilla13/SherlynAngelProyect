using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private string targetSpawnPointName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Validación para evitar el NullReferenceException si la instancia es nula
        if (SceneTransitionManager.Instance == null)
        {
            Debug.LogError("¡SceneTransitionManager.Instance es NULL! Comprueba que el objeto Manager esté en la escena y configurado en Awake.");
            return;
        }

        SceneTransitionManager.Instance.GoToScene(targetSceneName, targetSpawnPointName);
    }
}