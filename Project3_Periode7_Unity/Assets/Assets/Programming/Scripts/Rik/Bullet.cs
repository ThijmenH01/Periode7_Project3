using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 _BoxOffset;
    public Vector3 _BoxSize;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        transform.LookAt(player.transform.position);
    }

    private void Update()
    {
        if (CompareTag("PlayerBullet"))
        {
            transform.Translate(Vector3.back * 20f * Time.deltaTime);
        }
        else if (CompareTag("EnemyBullet"))
        {
            transform.Translate(Vector3.forward * 20f * Time.deltaTime);
        }

        Destroy(gameObject, 4);

        if (transform.position.z != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        Collider[] Colliders = Physics.OverlapBox(transform.position + _BoxOffset, _BoxSize);
        for (int i = 0; i < Colliders.Length; i++)
        {
            if (Colliders[i].CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            if (Colliders[i].CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
            if (Colliders[i].CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + _BoxOffset, _BoxSize);
    }
}