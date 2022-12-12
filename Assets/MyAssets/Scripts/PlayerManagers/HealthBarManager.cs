using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    #region Variables de la barra de salud del jugador/enemigo
    [SerializeField]
    private Image healthBarSprite;
    #endregion

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        #region Actualizar la barra de vida
        float healthPercentage = currentHealth / maxHealth;
        healthBarSprite.fillAmount = healthPercentage;
        #endregion
    }
    public void SetHealthBarColor(string hbColorType)
    {
        #region Establecer el color de la barra de salud
        if (hbColorType == "player") healthBarSprite.color = Color.green;
        else healthBarSprite.color = Color.red;  
        #endregion
    }


}
