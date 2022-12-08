using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace EnderDove
{
    public class Curse : MonoBehaviour
    {
        public Transform _curse;
        public Transform player;
        [SerializeField] private float timeToNextMove = 10f;

        private float _lastTime;

        public void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player" && Time.time - _lastTime > timeToNextMove)
            {
                float x = Random.Range(-5, 5) + 2 * _curse.position.x - player.position.x;
                float z = Random.Range(-5, 5) + 2 * _curse.position.z - player.position.z;
                _curse.DOMove(new Vector3(x, 1.25f, z), 0.1f);
                _lastTime = Time.time;
            }
        }
    }
}
