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
        private float _healthValue = 50f;
        [SerializeField] private float hpChangeSpeed = 10f;

        private void Update()
        {
            HealthSlider.value = Mathf.MoveTowards(HealthSlider.value, _healthValue, Time.deltaTime * hpChangeSpeed);
        }

        public void SetCurentHealth(int healthValue)
        {
            _healthValue = healthValue;
        }

        public void SetCurentStamina(int staminaValue)
        {
            StaminaSlider.value = staminaValue;
        }
    }
}
