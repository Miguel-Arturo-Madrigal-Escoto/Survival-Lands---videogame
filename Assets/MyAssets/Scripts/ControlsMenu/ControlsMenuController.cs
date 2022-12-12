using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsMenuController : MonoBehaviour
{
    public void LoadMainMenu()
    {
        // Ir al menú principal
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void LoadSelectionScreen()
    {
        // Ir a la selección de personaje
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
