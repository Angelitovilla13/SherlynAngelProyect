using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text[] menuItems;
    [SerializeField] private RectTransform arrow;

    [Header("Referencias de Cinemática")]
    [SerializeField] private ControladorCinematica controladorCinematica;
    [SerializeField] private int newGameOptionIndex = 1; // 0=Continuar, 1=Nueva Partida, 2=Ajustes, 3=Salir

    [Header("Colores")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = new Color(1f, 0.9f, 0.7f);

    [Header("Índice de la opción 'Salir' en el array")]
    [SerializeField] private int quitOptionIndex = 3;

    private int selectedIndex = 0;

    private void Start()
    {
        UpdateSelection();
        AsignarEventosClic();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex = (selectedIndex + 1) % menuItems.Length;
            UpdateSelection();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex = (selectedIndex - 1 + menuItems.Length) % menuItems.Length;
            UpdateSelection();
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ConfirmSelection();
        }
    }

    private void ConfirmSelection()
    {
        // Si la opción actual es "Nueva Partida" (índice 1)
        if (selectedIndex == newGameOptionIndex)
        {
            EjecutarNuevaPartida();
        }
        // Si la opción actual es "Salir" (índice 3)
        else if (selectedIndex == quitOptionIndex)
        {
            QuitGame();
        }
    }

    public void EjecutarNuevaPartida()
    {
        if (controladorCinematica != null)
        {
            controladorCinematica.IniciarCinematica();
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void UpdateSelection()
    {
        Vector2 pos = arrow.anchoredPosition;
        pos.y = menuItems[selectedIndex].rectTransform.anchoredPosition.y;
        arrow.anchoredPosition = pos;

        for (int i = 0; i < menuItems.Length; i++)
        {
            menuItems[i].color = (i == selectedIndex) ? highlightColor : normalColor;
        }
    }

    // Detecta automáticamente los clics del mouse sobre los textos del menú
    private void AsignarEventosClic()
    {
        for (int i = 0; i < menuItems.Length; i++)
        {
            int index = i; // Copia local del índice para la expresión lambda
            EventTrigger trigger = menuItems[i].gameObject.GetComponent<EventTrigger>();

            if (trigger == null)
            {
                trigger = menuItems[i].gameObject.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => {
                selectedIndex = index;
                UpdateSelection();
                ConfirmSelection();
            });

            trigger.triggers.Add(entry);
        }
    }
}