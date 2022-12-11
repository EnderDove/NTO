using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //дистанция от которой он начинает видеть игрока
    public float seeDistance = 20f;
    //дистанция до атаки
    public float attackDistance = 0;
    //скорость енеми
    public float speed = 4;
    //игрок
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
