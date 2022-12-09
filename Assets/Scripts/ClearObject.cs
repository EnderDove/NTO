using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnderDove
{
    public class ClearObject : MonoBehaviour
    {
        Curse curse;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                curse = FindObjectOfType<Curse>();
                curse.ClearCurse();
            }
        }
    }
}
