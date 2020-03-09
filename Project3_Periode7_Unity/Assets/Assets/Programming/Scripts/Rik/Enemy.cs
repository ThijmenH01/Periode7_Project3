using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public GameObject bullet;
    public Transform fireposition;
    private Rigidbody rb;
    public static Enemy instance;
    public int hp = 2;

    private void Start()
    {
        player = GameObject.Find("Player");

        StartCoroutine(BulletFire());

        rb = GetComponent<Rigidbody>();

        instance = this;
    }

    private void Update()
    {
        if (transform.position.z != 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, 0), Time.deltaTime * 3);
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 8)
        {
            Vector3 rotation = Quaternion.LookRotation(player.transform.position).eulerAngles;
            rotation.x = 0f;

            transform.rotation = Quaternion.Euler(rotation);
        }
        else
        {
            CheckForObstacle();
        }

        transform.GetChild(0).transform.LookAt(player.transform.position);

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CheckForObstacle()
    {
        rb.AddForce(600 * Time.deltaTime, 0, 0);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 2f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.blue);
            Debug.Log("Did hit");
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