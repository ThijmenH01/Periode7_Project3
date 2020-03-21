using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private float fireRate = 7.5f;
    [SerializeField] private PlayerScript player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private CinemachineVirtualCamera vcam;
    private float shootTimer;
    private float nextTimeToFire = 0f;

    private void Start()
    {
        player = transform.parent.GetComponentInParent<PlayerScript>();
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
    }

    private void Update()
    {
        MoveGun();

        if (PlayerScript.instance.playerDir != PlayerScript.PlayerDir.Neutral)
        {
            if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire || Input.GetKey(KeyCode.Joystick1Button7) && Time.time >= nextTimeToFire)
            {
                vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.5f;
                Shoot();
                Recoil();
            }
            else
            {
                vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            }
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

            if (PlayerScript.instance.playerDir != PlayerScript.PlayerDir.Neutral)
            {
                //if (transform.eulerAngles.x < 50f && transform.eulerAngles.x > -50)
                //{
                transform.LookAt(new Vector3(pointToLook.x, pointToLook.y, transform.position.z));
                //}
            }

            if (Input.mousePosition.x < Camera.main.WorldToScreenPoint(player.transform.position).x)
            {
                PlayerScript.instance.playerDir = PlayerScript.PlayerDir.Left;
            }
            else
            {
                PlayerScript.instance.playerDir = PlayerScript.PlayerDir.Right;
            }
        }
    }

    private void Shoot()
    {
        GameObject Playerbullet = Instantiate(bullet, transform.GetChild(0).transform.position, Quaternion.identity);
        Playerbullet.tag = "PlayerBullet";
        nextTimeToFire = Time.time + 1f / fireRate;
    }

    private void Recoil()
    {
        Vector2 pos = player.transform.position;
        if (PlayerScript.instance.playerDir == PlayerScript.PlayerDir.Left)
        {
            pos.x = Mathf.Lerp(player.transform.position.x, player.transform.position.x + 5, 1f * Time.deltaTime);
        }

        if (PlayerScript.instance.playerDir == PlayerScript.PlayerDir.Right)
        {
            pos.x = Mathf.Lerp(player.transform.position.x, player.transform.position.x - 5, 1f * Time.deltaTime);
        }
        player.transform.position = pos;
    }
}