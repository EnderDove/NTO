using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace EnderDove
{
    public class Curse : MonoBehaviour
    {
        public Transform _curse;
        public GameObject player;
        public GameObject totem;
        private GameObject clear;
        public Image UICurse;
        PlayerManager _player;
        [SerializeField] private float timeToNextMove = 10f;

        private bool _l1 = true;
        private bool _l2 = true;

        private bool _activavtion = true;
        private float _lastTime;

        public void Start()
        {
                _player = player.GetComponent<PlayerManager>();
        }

        public void OnTriggerStay(Collider other)
        {
            if (_player.stacks == _player.s && _l1)
            {
                Renderer _clearRend = clear.GetComponent<MeshRenderer>();
                Renderer _amuletRend = GetComponentInChildren<MeshRenderer>();
                _clearRend.enabled = false;
                _amuletRend.enabled = false;
                _l1 = false;
            }

            if (_player.stacks == 2 * _player.s && _l2)
            {
                UICurse.color = new Color(1f, 1f, 1f, 0f);
                _l2 = false;
            }

            if (other.tag == "Player" && _activavtion)
            {
                _activavtion = false;
                float x = 2 * _curse.position.x - player.transform.position.x;
                float z = 2 * _curse.position.z - player.transform.position.z;
                Vector3 pos = new Vector3(x, 2.99f, z);
                clear = Instantiate(totem, pos, Quaternion.identity);
                clear.transform.LookAt(_curse.position, Vector3.up);
                Transform _clear = clear.GetComponent<Transform>();
                _clear.rotation = Quaternion.Euler(-90, _clear.rotation.y + 90, _clear.rotation.z);
                UICurse.color = new Color(1f, 1f, 1f, 1f);
            }

            if (other.tag == "Player" && Time.time - _lastTime > timeToNextMove)
            {
                float x = Random.Range(-5, 5) + 2 * _curse.position.x - player.transform.position.x;
                float z = Random.Range(-5, 5) + 2 * _curse.position.z - player.transform.position.z;
                _curse.DOMove(new Vector3(x, 1.25f, z), 0.1f);
                _lastTime = Time.time;
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
