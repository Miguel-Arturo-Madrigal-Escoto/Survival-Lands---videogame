using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    #region Sprites de las vidas
    [SerializeField]
    private GameObject[] _hearts;
    #endregion
    

    public void UpdateLivesImages(int _lives)
    {
        #region Actualizar la imagen de las vidas
        switch (_lives)
        {
            case 3:
                _hearts[0].SetActive(false);
                _hearts[1].SetActive(false);
                _hearts[2].SetActive(true);
                break;
            case 2:
                _hearts[0].SetActive(false);
                _hearts[1].SetActive(true);
                _hearts[2].SetActive(false);
                break;
            case 1:
                _hearts[0].SetActive(true);
                _hearts[1].SetActive(false);
                _hearts[2].SetActive(false);
                break;
            case 0:
                _hearts[0].SetActive(false);
                _hearts[1].SetActive(false);
                _hearts[2].SetActive(false);
                break;
        }
        #endregion
    }
}
