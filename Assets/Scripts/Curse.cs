using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace EnderDove
{
    public class Curse : MonoBehaviour
    {
        public Transform _curse;
        public Transform player;
        public GameObject totem;
        private GameObject clear;
        public Image UICurse;
        [SerializeField] private float timeToNextMove = 10f;

        private bool _activavtion = true;
        private float _lastTime;

        public void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player" && Time.time - _lastTime > timeToNextMove)
            {
                float x = Random.Range(-5, 5) + 2 * _curse.position.x - player.position.x;
                float z = Random.Range(-5, 5) + 2 * _curse.position.z - player.position.z;
                _curse.DOMove(new Vector3(x, 1.25f, z), 0.1f);
                _lastTime = Time.time;
                UICurse.color = new Color(1f, 1f, 1f, 1f);
            }

            if (_activavtion)
            {
                _activavtion = false;
                float k = (player.position.z - _curse.position.z) / (player.position.x - _curse.position.x);
                float b = _curse.position.z - k * _curse.position.x;

                float cx = _curse.position.x;

                if (player.position.x - _curse.position.x > 0)
                {
                    cx = Random.Range(-5, 2) + _curse.position.x;
                }
                else
                {
                    cx = Random.Range(2, 5) + _curse.position.x;
                }
                Vector3 pos = new Vector3(cx, 2.99f, k*cx + b);
                clear = Instantiate(totem, pos, Quaternion.identity);
                clear.GetComponent<Transform>().Rotate(new Vector3(-90, 0, 0)); ;
            }
        }
    }
}
