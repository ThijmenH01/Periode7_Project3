using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public GameObject bullet;
    public Transform fireposition;
    public static int hp = 2;

    private void Start()
    {
        player = GameObject.Find("Player");

        StartCoroutine(BulletFire());
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

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator BulletFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            if (Vector3.Distance(transform.position, player.transform.position) < 8)
            {
                Instantiate(bullet, fireposition.position, Quaternion.identity);
            }
        }
    }
}