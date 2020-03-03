using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    private Player player;
    [SerializeField] private GameObject bullet;
    private float shootTimer;

    private void Start()
    {
    }

    private void Update()
    {
        MoveGun();

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void MoveGun()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayLength;
        Plane groundPlane = new Plane(Vector3.forward, Vector3.zero);

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            //if ()
            //{
            transform.LookAt(new Vector3(pointToLook.x, pointToLook.y, transform.position.z));
            //}
        }
    }

    private void Shoot()
    {
        if (shootTimer <= 0f)
        {
            GameObject Playerbullet = Instantiate(bullet, transform.GetChild(0).transform.position, Quaternion.identity);
            Playerbullet.tag = "PlayerBullet";
            shootTimer = 0.3f;
        }
        else
        {
            shootTimer -= Time.deltaTime;
        }
    }
}