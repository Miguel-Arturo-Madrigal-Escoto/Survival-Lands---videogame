using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Sprites de los hostages
    [SerializeField]
    private GameObject[] _hostagesSprites;
    #endregion
    
    public void UpdateSalvaditosImages(int _hostages)
    {
        switch (_hostages)
        {
            case 0:
                _hostagesSprites[0].SetActive(true);
                _hostagesSprites[1].SetActive(false);
                _hostagesSprites[2].SetActive(false);
                _hostagesSprites[3].SetActive(false);
                break;
            case 1:
                _hostagesSprites[0].SetActive(false);
                _hostagesSprites[1].SetActive(true);
                _hostagesSprites[2].SetActive(false);
                _hostagesSprites[3].SetActive(false);
                break;
            case 2:
                _hostagesSprites[0].SetActive(false);
                _hostagesSprites[1].SetActive(false);
                _hostagesSprites[2].SetActive(true);
                _hostagesSprites[3].SetActive(false);
                break;
            case 3:
                _hostagesSprites[0].SetActive(false);
                _hostagesSprites[1].SetActive(false);
                _hostagesSprites[2].SetActive(false);
                _hostagesSprites[3].SetActive(true);
                break;
        }
    }
}
