using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnderDove
{
    public class PlayerLocomotion : MonoBehaviour
    {
        PlayerManager playerManager;
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        [HideInInspector]
        public Transform playerTransform;
        [HideInInspector]
        public AnimatorHandler animatorHandler;
        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Movement Stats")]
        [SerializeField] private float movementSpeed = 5;
        [SerializeField] private float sprintSpeed = 7;
        [SerializeField] private float rotationSpeed = 10;
        [SerializeField] private float rollStaminaConsumption = 15f;

        void Start()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            cameraObject = Camera.main.transform;
            playerTransform = transform;
            animatorHandler.Initialize();
        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetVector;

        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moreOvveride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horisontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if(targetDir == Vector3.zero)
                targetDir = playerTransform.forward;
            

            float rs = rotationSpeed;
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(playerTransform.rotation, tr, rs * delta);

            playerTransform.rotation = targetRotation;
        }

        public void HandleMovement(float delta)
        {
            if(inputHandler.rollFlag)
                return;
            
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horisontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = movementSpeed;
            if(inputHandler.sprintFlag)
            {
                speed = sprintSpeed;
                playerManager.isSprinting = true;
            }
            moveDirection *= speed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);

            if(animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }
        }

        public void HandleRollingAndSprinting(float delta)
        {
            if(animatorHandler.anim.GetBool("isInteracting"))
                return;

            if(inputHandler.rollFlag)
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection = cameraObject.right * inputHandler.horisontal;

<<<<<<< Updated upstream
                if(inputHandler.moveAmount > 0)
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
                playerManager.SubtractStaminaValue(rollStaminaConsumption);

>>>>>>> Stashed changes
=======
                playerManager.SubtractStaminaValue(rollStaminaConsumption);

>>>>>>> Stashed changes
=======
                playerManager.SubtractStaminaValue(rollStaminaConsumption);

>>>>>>> Stashed changes
=======
                playerManager.SubtractStaminaValue(rollStaminaConsumption);

>>>>>>> Stashed changes
                if (inputHandler.moveAmount > 0)
>>>>>>> Stashed changes
                {
                    animatorHandler.PlayTargetAnimation("Rolling", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    playerTransform.rotation = rollRotation;
                }
                else
                {
                    animatorHandler.PlayTargetAnimation("Backstep", true);
                }
            }
        }

    #endregion
    }
}
