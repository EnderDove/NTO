using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EnderDove
{
    public class HealthAndStaminaBar : MonoBehaviour
    {
        public Slider HealthSlider;
        public Slider StaminaSlider;



        public void SetCurentHealth(int healthValue)
        {
            HealthSlider.value = healthValue;
        }

        public void SetCurentStamina(int staminaValue)
        {
            StaminaSlider.value = staminaValue;
        }
    }
}
