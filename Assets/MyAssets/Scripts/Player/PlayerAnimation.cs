using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnimator;

    private void Start()
    {
        this._playerAnimator = GetComponentInChildren<Animator>();

        if (this._playerAnimator == null)
        {
            Debug.LogError("No se encontro el Animator en el personaje");
        }
    }

    public void SetSpeed(float speed)
    {
        this._playerAnimator.SetFloat("Speed", speed);
    }

    public void SetJump()
    {
        this._playerAnimator.SetTrigger("Jump_trigger");
    }
    public void SetDoubleJump()
    {
        this._playerAnimator.SetTrigger("Jump2_trigger");
    }
    public void SetDeath()
    {
        this._playerAnimator.SetTrigger("Die_trigger");
    }

    public void SetRespawn()
    {
        this._playerAnimator.SetTrigger("Respawn_Trigger");
    }

    public void SetAttack()
    {
        this._playerAnimator.SetTrigger("Attack_trigger");
    }
}
