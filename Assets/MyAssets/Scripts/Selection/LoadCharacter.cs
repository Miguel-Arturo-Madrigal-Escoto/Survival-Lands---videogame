using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    #region Variables de los personajes

    [SerializeField]
    private GameObject[] characterPrefabs;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    //private TMP_Text label;

    #endregion

    void Start()
    {
        #region Cargar al personaje seleccionado
        // Leer la data de PlayerPrefs denominada "SelectedCharacter". Si no existe, 0 (primer personaje)
        int selectedCharacter = PlayerPrefs.GetInt("Selected_Character");

        if (selectedCharacter == 0)
        {
            this.characterPrefabs[0].SetActive(true);
            this.characterPrefabs[1].SetActive(false);
        }
        else
        {
            this.characterPrefabs[0].SetActive(false);
            this.characterPrefabs[1].SetActive(true);
        }
 
        #endregion
    }
}
