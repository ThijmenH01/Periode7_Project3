using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public GameObject bullet;
    public Transform fireposition;
    public static Enemy instance;
    public int hp = 2;

    private void Start()
    {
        player = GameObject.Find("Player");

        StartCoroutine(BulletFire());

        instance = this;
    }

    private void Update()
    {
        if (transform.position.z != 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, 0), Time.deltaTime * 3);
        }

        Vector3 rotation = Quaternion.LookRotation(player.transform.position).eulerAngles;
        rotation.x = 0f;

        transform.rotation = Quaternion.Euler(rotation);

        transform.GetChild(0).transform.LookAt(player.transform.position);
        CheckForObstacle();

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CheckForObstacle()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            Debug.Log("Did Hit");
        }
    }

    private IEnumerator BulletFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Vector3.Distance(transform.position, player.transform.position) < 8)
            {
                GameObject enemyBullet = Instantiate(bullet, fireposition.position, Quaternion.identity);
                enemyBullet.tag = "EnemyBullet";
            }
        }
    }
}