using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //��������� �� ������� �� �������� ������ ������
    public float seeDistance = 20f;
    //��������� �� �����
    public float attackDistance = 0;
    //�������� �����
    public float speed = 4;
    //�����
    private Transform target;

    private bool p;
    private float lt;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (!p && Time.time - lt > 1f)
        {
            p = true;
        }

        if (Vector3.Distance(transform.position, target.transform.position) < seeDistance && p)
        {
                transform.LookAt(target.transform);
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            p = false;
            lt = Time.time;
        }
    }
}
