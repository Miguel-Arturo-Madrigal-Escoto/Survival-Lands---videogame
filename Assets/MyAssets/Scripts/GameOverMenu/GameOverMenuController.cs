using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    public void LoadMainMenu()
    {
        // Ir al menun principal
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void LoadSelectionScreen()
    {
        // Ir a la selecci�n de personajes
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        // Cerrar la aplicaci�n
        Application.Quit();
    }
}
