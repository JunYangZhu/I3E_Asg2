using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    /// <summary>
    /// Set stamina to max stamina
    /// </summary>
    /// <param name="stamina"></param>
    // Start is called before the first frame update
    public void SetMaxStamina(float stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
    }

    /// <summary>
    /// updates stamina slider when stamina is consumed
    /// </summary>
    /// <param name="stamina"></param>
    // Update is called once per frame
    public void SetStamina(float stamina)
    {
        slider.value = stamina;

    }
}
