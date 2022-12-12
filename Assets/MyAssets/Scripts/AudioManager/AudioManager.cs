using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Variables de audio
    [SerializeField]
    private AudioSource[] src;
    [SerializeField]
    private AudioClip[] clip;
    #endregion

    public void PlayAudio(string sound)
    {
        #region Reproducir audio especificado
        switch(sound)
        {
            case "PlayerHit":
                src[0].enabled = true;
                src[0].PlayOneShot(clip[0]);
                break;

            case "PlayerAttack":
                src[1].enabled = true;
                src[1].PlayOneShot(clip[1]);
                break;

            case "PlayerScore":
                src[2].enabled = true;
                src[2].PlayOneShot(clip[2]);
                break;

            case "PlayerJump":
                src[3].enabled = true;
                src[3].PlayOneShot(clip[3]);
                break;

            case "PlayerHeal":
                src[4].enabled = true;
                src[4].PlayOneShot(clip[4]);
                break;

            case "EnemyDamage":
                src[5].enabled = true;
                src[5].PlayOneShot(clip[5]);
                break;

            case "EnterPortal":
                src[6].enabled = true;
                src[6].PlayOneShot(clip[6]);
                break;
        }
        #endregion
    }
}
