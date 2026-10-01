using UnityEngine;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text[] menuItems;
    [SerializeField] private RectTransform arrow;

    [Header("Colores")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = new Color(1f, 0.9f, 0.7f);

    [Header("Índice de la opción 'Salir' en el array")]
    [SerializeField] private int quitOptionIndex = 3; // 0=Continuar, 1=Nueva Partida, 2=Ajustes, 3=Salir

    private int selectedIndex = 0;

    private void Start()
    {
        UpdateSelection();
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
        if (selectedIndex == quitOptionIndex)
        {
            QuitGame();
        }
        // Las demás opciones (Continuar, Nueva Partida, Ajustes) no hacen nada todavía
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() no funciona dentro del Editor, así que detenemos el Play Mode en su lugar
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
}