using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadPlayScreen()
    {
        // Ir a la selección de personajes
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void LoadControlsScreen()
    {
        SceneManager.LoadScene(5, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        // Cerrar la aplicación
        Application.Quit();
    }
}
