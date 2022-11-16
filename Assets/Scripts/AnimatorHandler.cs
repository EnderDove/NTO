using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AnimatorHandler : MonoBehaviour
    {
        public Animator anim;
        int vertical;
        int horisontal;
        public bool canRotate;

        public void Initialize()
        {
            anim = GetComponent<Animator>();
            vertical = Animator.StringToHash("Vertical");
            horisontal = Animator.StringToHash("Horisontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horisontalMovement)
        {
            #region Vertical
            float v = 0;

            if(verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if(verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if(verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = - 0.5f;
            }
            else if(verticalMovement < -0.55f)
            {
                v = -1;
            }
            #endregion

            #region Horizontal
            float h = 0;

            if(horisontalMovement > 0 && horisontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if(horisontalMovement > 0.55f)
            {
                h = 1;
            }
            else if(horisontalMovement < 0 && horisontalMovement > -0.55f)
            {
                h = - 0.5f;
            }
            else if(horisontalMovement < -0.55f)
            {
                h = -1;
            }
            #endregion

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horisontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotate()
        {
            canRotate = false;
        }
    }
}