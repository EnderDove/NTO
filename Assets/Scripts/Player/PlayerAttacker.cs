using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnderDove
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private float lightAttackStaminaConsumption = 15;
        [SerializeField] private float heavyAttackStaminaConsumption = 25;

        AnimatorHandler animatorHandler;
        PlayerManager playerManager;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            animatorHandler.PlayTargetAnimation(weapon.OH_light_Attack, true);
            playerManager.ChangeStaminaValue(lightAttackStaminaConsumption);
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            animatorHandler.PlayTargetAnimation(weapon.OH_heavy_Attack, true);
            playerManager.ChangeStaminaValue(heavyAttackStaminaConsumption);
        }
    }
}
