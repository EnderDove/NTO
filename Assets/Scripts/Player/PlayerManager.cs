using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnderDove
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;

        [Header("Stamina and Health")]
        [SerializeField] private float maxStaminaValue = 100f;
        [SerializeField] private float maxHealthValue = 50f;
        [SerializeField] private float StaminaRegen = 5f;
        [SerializeField] private float DeltaTimeToAddStamina = 1f;

        private float _lastTimeSubstarctingStaminaValue;

        public float StaminaValue = 100f;
        public float HealthValue = 50f;

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;

        private void Awake()
        {
            cameraHandler = CameraHandler.singleton;
        }

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
        }

        void Update()
        {
            float delta = Time.fixedDeltaTime;
            
            isInteracting = anim.GetBool("isInteracting");
            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
            RegenStamina(delta);
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
            isSprinting = inputHandler.b_Input;
        }

        public void RegenStamina(float value)
        {
            if (Time.time - _lastTimeSubstarctingStaminaValue < DeltaTimeToAddStamina && StaminaValue < maxStaminaValue)
            {
                StaminaValue += value * StaminaRegen;
            }
        }

        public void SubtractStaminaValue(float value)
        {
            _lastTimeSubstarctingStaminaValue = Time.time;
            StaminaValue -= value;
        }
    }
}