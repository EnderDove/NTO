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

        [Header("Curse")]
        [SerializeField] private float timeToAddStack = 5f;
        [SerializeField] private float StackValue = 0.9f;

        [Header("Stamina and Health")]
        [SerializeField] private float StaminaRegen = 5f;
        [SerializeField] private float DeltaTimeToAddStamina = 1f;
        [SerializeField] private float RunningStaminaConsumption = 1f;

        private float _lastTimeSubstarctingStaminaValue;
        private float _lastTimeAddCurse;

        [SerializeField] private float StandartStaminaValue = 100f;
        [SerializeField] private float StandartHealthValue = 50f;

        public float maxStaminaValue = 100f;
        public float StaminaValue = 100f;
        public float maxHealthValue = 50f;
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
            healthAndStaminaBar = FindObjectOfType<HealthAndStaminaBar>();
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
            SetMaxHealthValue(StandartHealthValue);
            SetMaxStaminaValue(StandartStaminaValue);
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

            if (isSprinting)
            {
                ChangeStaminaValue(-RunningStaminaConsumption);
            }

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

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Curse" && Time.time - _lastTimeAddCurse > timeToAddStack)
            {
                _lastTimeAddCurse = Time.time;
                AddCurse();
            }
        }

        public void AddCurse()
        {
            SetMaxHealthValue(maxHealthValue * StackValue);
            SetMaxStaminaValue(maxStaminaValue * StackValue);
            StaminaRegen = StaminaRegen * StackValue;
        }

        public void SetMaxHealthValue(float maxHealth)
        {
            maxHealthValue = maxHealth;

            if (HealthValue > maxHealthValue)
            {
                SetHealthValue(maxHealthValue);
            }

            healthAndStaminaBar.HealthSlider.maxValue = (int)Mathf.Round(maxHealth);
        }

        public void SetHealthValue(float health)
        {
            HealthValue = health;
            healthAndStaminaBar.SetCurentHealth((int)Mathf.Round(health));
        }

        public void TakeDamage(float damageValue)
        {
            SetHealthValue(HealthValue - damageValue);

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

        public void SetMaxStaminaValue(float maxStamina)
        {
            maxStaminaValue = maxStamina;

            if (StaminaValue > maxStaminaValue)
            {
                ChangeStaminaValue(maxStaminaValue);
            }

            healthAndStaminaBar.StaminaSlider.maxValue = (int)Mathf.Round(maxStamina);
        }
    }
}