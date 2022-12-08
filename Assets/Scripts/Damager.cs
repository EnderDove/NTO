using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnderDove
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] private float StandartDamage;
        PlayerManager playerManager;

        void Start()
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
                playerManager.TakeDamage(StandartDamage);
            }
        }
    }
}
