using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class ControladorCinematica : MonoBehaviour
{
    [Header("Configuración de Escena")]
    [SerializeField] private string nombreEscenaSala = "Sala";

    [Header("Referencias de UI")]
    [SerializeField] private GameObject pantallaCinematica;
    [SerializeField] private GameObject fondoNegro;

    [Header("Componentes de Cinemática")]
    [SerializeField] private PlayableDirector directorCinematica;

    public void IniciarCinematica()
    {
        if (fondoNegro != null)
        {
            fondoNegro.SetActive(true);
        }

        if (pantallaCinematica != null)
        {
            pantallaCinematica.SetActive(true);
        }

        if (directorCinematica != null)
        {
            directorCinematica.Play();
        }
    }

    public void CargarSala()
    {
        SceneManager.LoadScene(nombreEscenaSala);
    }
}