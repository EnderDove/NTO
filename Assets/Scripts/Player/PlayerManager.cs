using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnderDove
{
    public class PlayerManager : MonoBehaviour
    {
        HealthAndStaminaBar healthAndStaminaBar;
        InputHandler inputHandler;
        Animator anim;
        AnimatorHandler animatorHandler;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;

        [Header("Stamina and Health")]
        public float maxStaminaValue = 100f;
        public float maxHealthValue = 50f;
        [SerializeField] private float StaminaRegen = 5f;
        [SerializeField] private float DeltaTimeToAddStamina = 1f;
        [SerializeField] private float RunningStaminaConsumption = 1f;

        private float _lastTimeSubstarctingStaminaValue;

        public float StaminaValue = 100f;
        public float HealthValue = 50f;

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;

        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
        }

        void Start()
        {
            healthAndStaminaBar = GetComponent<HealthAndStaminaBar>();
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
            SetMaxHealthValue(maxStaminaValue);
        }

        void Update()
        {
            float delta = Time.fixedDeltaTime;
            
            isInteracting = anim.GetBool("isInteracting");
            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            RegenStamina(delta);

            if (isSprinting)
            {
                ChangeStaminaValue(delta);
            }
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }
        }

        public void SetMaxHealthValue(float maxHealth)
        {
            maxHealthValue = maxHealth;
            healthAndStaminaBar.HealthSlider.maxValue = (int)Mathf.Round(maxHealth);
        }

        public void SetHealthValue(float health)
        {
            HealthValue = health;
            healthAndStaminaBar.SetCurentHealth((int)Mathf.Round(health));
        }

        public void TakeDamage(float damageValue)
        {
            HealthValue -= damageValue;
            SetHealthValue((int)Mathf.Round(HealthValue));

            animatorHandler.PlayTargetAnimation("Damage", true);

            if (HealthValue <= 0)
            {
                animatorHandler.PlayTargetAnimation("Death", true);
            }
        }

        public void RegenStamina(float value)
        {
            if (Time.time - _lastTimeSubstarctingStaminaValue < DeltaTimeToAddStamina && StaminaValue < maxStaminaValue)
            {
                StaminaValue += value * StaminaRegen;
                healthAndStaminaBar.SetCurentStamina((int)Mathf.Round(StaminaValue));
            }
        }

        public void ChangeStaminaValue(float value)
        {
            _lastTimeSubstarctingStaminaValue = Time.time;
            StaminaValue += value;
            healthAndStaminaBar.SetCurentStamina((int)Mathf.Round(StaminaValue));
        }
    }
}