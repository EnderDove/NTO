using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLocomotion : MonoBehaviour
    {
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        [HideInInspector]
        public Transform playerTransform;
        [HideInInspector]
        public AnimatorHandler animatorHandler;
        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            cameraObject = Camera.main.transform;
            playerTransform = transform;
            animatorHandler.Initialize();
        }

        public void Update()
        {
            float delta = Time.deltaTime;

            inputHandler.TickInput(delta);

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horisontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = movementSpeed;
            moveDirection *= speed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            if(animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }
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

    #endregion
    }
}
