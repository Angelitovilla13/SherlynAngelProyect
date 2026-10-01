using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    public string TargetSpawnPointName { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Este método corre AUTOMÁTICAMENTE antes de que cargue cualquier escena,
    // sin importar si le diste Play desde Escena1, sala, o cualquier otra.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnsureManagerExists()
    {
        if (Instance != null) return; // ya existe, no hacemos nada

        GameObject prefab = Resources.Load<GameObject>("Managers"); // el nombre del archivo Prefab, sin extensión
        if (prefab == null)
        {
            Debug.LogError("No se encontró el Prefab 'Managers' en la carpeta Resources.");
            return;
        }

        Instantiate(prefab);
    }

    public void GoToScene(string sceneName, string spawnPointName)
    {
        TargetSpawnPointName = spawnPointName;
        SceneManager.LoadScene(sceneName);
    }
}