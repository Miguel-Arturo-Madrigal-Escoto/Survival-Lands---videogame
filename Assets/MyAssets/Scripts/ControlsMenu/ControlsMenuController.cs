using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsMenuController : MonoBehaviour
{
    public void LoadMainMenu()
    {
        // Ir al men� principal
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void LoadSelectionScreen()
    {
        // Ir a la selecci�n de personaje
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
