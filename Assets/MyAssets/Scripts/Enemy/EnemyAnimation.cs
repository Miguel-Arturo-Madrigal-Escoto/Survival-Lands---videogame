using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el Animator que esta en el enemigo 
        this._enemyAnimator = GetComponentInChildren<Animator>();

        if (this._enemyAnimator == null)
        {
            Debug.LogError("No se encontro el Animator en el enemigo");
        }
    }

    public void SetWalk()
    {
        this._enemyAnimator.SetTrigger("Walk Forward");
    }

    public void SetIdle()
    {
        this._enemyAnimator.ResetTrigger("Walk Forward");
    }
    public void SetAttack()
    {
        this._enemyAnimator.SetTrigger("Attack 01");
    }

    public void SetDie()
    {
        this._enemyAnimator.SetTrigger("Die");
    }
}
