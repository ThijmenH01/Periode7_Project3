using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 _BoxSize;
    private Vector3 dir;
    private Vector3 rayDir;
    private GameObject player;
    public GameObject bullet;
    public Transform fireposition;
    private Rigidbody rb;
    public static Enemy instance;
    public int hp = 3;
    private bool switchSide;

    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(BulletFire());
        rb = GetComponent<Rigidbody>();
        instance = this;
        Vector3 rotation = Quaternion.LookRotation(player.transform.position).eulerAngles;
        rotation.x = 0f;
        transform.rotation = Quaternion.Euler(rotation);
        int side = Random.Range(0, 2);
        if (side == 1)
            switchSide = true;
        else if (side == 2)
            switchSide = false;
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

        DidHit();

        transform.GetChild(0).transform.LookAt(player.transform.position);

        if (hp <= 0)
        {
            Destroy(gameObject);
            WaveSystem.Instance.enemiesAlive.Remove(this.gameObject);
        }
    }

    private void CheckForObstacle()
    {
        switch (switchSide)
        {
            case true:
                dir = Vector3.right;
                rayDir = Vector3.right;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 10);
                break;

            case false:
                dir = Vector3.left;
                rayDir = Vector3.forward;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 10);
                break;
        }

        transform.Translate(dir * 5 * Time.deltaTime, Space.World);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformPoint(rayDir), out hit, 1.5f))
        {
            if (!hit.collider.CompareTag("Enemy"))
            {
                if (switchSide == true)
                    switchSide = false;
                else if (switchSide == false)
                    switchSide = true;
            }
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(rayDir) * 1f, Color.blue);
    }

    private void DidHit()
    {
        Collider[] Colliders = Physics.OverlapBox(transform.position + new Vector3(0, 0, 0), _BoxSize);
        for (int i = 0; i < Colliders.Length; i++)
        {
            if (Colliders[i].CompareTag("PlayerBullet"))
            {
                hp -= 1;
            }
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