using DG.Tweening;
using UnityEngine;
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
            if (_activavtion)
            {
                _activavtion = false;
                float x = 2 * _curse.position.x - player.position.x;
                float z = 2 * _curse.position.z - player.position.z;
                Vector3 pos = new Vector3(x, 2.99f, z);
                clear = Instantiate(totem, pos, Quaternion.identity);
                clear.transform.LookAt(_curse.position, Vector3.up);
                Transform _clear = clear.GetComponent<Transform>();
                _clear.rotation = Quaternion.Euler(-90, _clear.rotation.y + 90, _clear.rotation.z);
            }

            if (other.tag == "Player" && Time.time - _lastTime > timeToNextMove)
            {
                float x = Random.Range(-5, 5) + 2 * _curse.position.x - player.position.x;
                float z = Random.Range(-5, 5) + 2 * _curse.position.z - player.position.z;
                _curse.DOMove(new Vector3(x, 1.25f, z), 0.1f);
                _lastTime = Time.time;
                UICurse.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        public void ClearCurse()
        {
            FindObjectOfType<PlayerManager>().ResetCurse();
            UICurse.color = new Color(1f, 1f, 1f, 0f);
            Destroy(FindObjectOfType<Curse>().gameObject);
            Destroy(clear);
        }
    }
}
