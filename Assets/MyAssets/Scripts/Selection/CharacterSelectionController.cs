using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionController : MonoBehaviour
{
    #region Variables de la selección de personajes
    [SerializeField]
    private GameObject[] characters;
    private int selectedCharacter = 0;
    #endregion

    public void Update()
    {
        if (selectedCharacter == 0)
        {
            this.characters[0].SetActive(true);
            this.characters[1].SetActive(false);
        }
        else
        {
            this.characters[0].SetActive(false);
            this.characters[1].SetActive(true);
        }
    }

    public void NextCharacter()
    {
        #region Cambiar al siguiente personaje
        // Ocultar el personaje actual
        this.characters[selectedCharacter].SetActive(false);

        // Seleccionar el siguiente personaje
        this.selectedCharacter = (selectedCharacter + 1) % characters.Length;

        // Mostrar el personaje seleccionado
        this.characters[selectedCharacter].SetActive(true);
        #endregion
    }

    public void PreviousCharacter()
    {
        #region Cambiar al personaje previo
        // Ocultar el personaje actual
        this.characters[selectedCharacter].SetActive(false);
        --selectedCharacter;

        // Si es el primer personaje, seleccionar el último (aqui se iria a -1)
        if (this.selectedCharacter < 0)
        {
            // this.selectedCharacter = this.characters.Length - 1; ó
            this.selectedCharacter += this.characters.Length;
        }

        // Mostrar el personaje seleccionado
        this.characters[selectedCharacter].SetActive(true);
        #endregion
    }


    public void StartGame()
    {
        #region Comienzo del juego una vez seleccionado el personaje deseado
        // PlayerPref: Almacenamiento de datos en el dispositivo
        // (en este caso, el personaje seleccionado) entre escenas
        PlayerPrefs.SetInt("Selected_Character", this.selectedCharacter);

        // Cargar la escena del juego
        // Quita todas las escenas cargadas para cargar la especificada
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        #endregion
    }

    public void GoBackToMainMenu()
    {
        // Ir al menu principal
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
