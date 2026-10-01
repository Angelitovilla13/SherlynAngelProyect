using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void Start()
    {
        if (SceneTransitionManager.Instance == null) return;

        string spawnName = SceneTransitionManager.Instance.TargetSpawnPointName;
        if (string.IsNullOrEmpty(spawnName)) return; // primera vez que arranca el juego, sin transición previa

        GameObject spawnPoint = GameObject.Find(spawnName);
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
        else
        {
            Debug.LogWarning($"No se encontró el Spawn Point llamado '{spawnName}' en esta escena.");
        }
    }
}