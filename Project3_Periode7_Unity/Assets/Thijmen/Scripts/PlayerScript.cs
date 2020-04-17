using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float rotSpeed;
    private RectTransform healthBar;
    public PlayerDir playerDir;
    private JetPack jetpack;
    private Rigidbody rb;
    private float rotValue;
    public static PlayerScript instance;
    public static float hp = 20;
    public bool SMGPickedUp;

    public enum PlayerDir
    {
        Neutral = 0,
        Right,
        Left
    };

    private void Start()
    {
        instance = this;
        jetpack = GetComponentInChildren<JetPack>();
        rb = GetComponentInChildren<Rigidbody>();
        healthBar = GameObject.Find("HealthBar").GetComponent<RectTransform>();
    }

    private void Update()
    {
        rotValue = Mathf.Clamp(rb.velocity.magnitude * 10, 0, 40);
        EnumBehaviour();

        if (hp <= 0)
        {
            Death();
        }

        if (PlayerInput(KeyCode.Space) || PlayerInput(KeyCode.JoystickButton0) || Input.GetMouseButton(1))
        {
            jetpack.UseJetPackFuel();

            if (jetpack.allowedToBoost)
            {
                StartCoroutine(jetpack.DragAsync());
            }
        }
        else
        {
            jetpack.smoke.Stop();
            jetpack.sparks.Stop();
        }

        switch (playerDir)
        {
            case PlayerDir.Neutral:
                rotValue = 0;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotSpeed);
                if (transform.position.z != 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, 0), Time.deltaTime * rotSpeed);
                }
                break;

            case PlayerDir.Right:
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-rotValue, -90, 0), Time.deltaTime * rotSpeed);
                break;

            case PlayerDir.Left:
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-rotValue, 90, 0), Time.deltaTime * rotSpeed);
                break;
        }
        healthBar.localScale = new Vector3(hp / 20, 1, 1);

        if (SMGPickedUp)
        {
        }
    }

    private void EnumBehaviour()
    {
        if (Input.mousePosition.x < Camera.main.WorldToScreenPoint(transform.position).x)
        {
            playerDir = PlayerDir.Left;
        }
        else
        {
            playerDir = PlayerDir.Right;
        }

        if (Input.GetAxis("Controller Axis") == -1)
        {
            playerDir = PlayerDir.Left;
        }
        else if (Input.GetAxis("Controller Axis") == 1)
        {
            playerDir = PlayerDir.Right;
        }
    }

    private void FixedUpdate()
    {
        if (PlayerInput(KeyCode.Space) || PlayerInput(KeyCode.JoystickButton0) || Input.GetMouseButton(1))
        {
            jetpack.JetPackMovement();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (!jetpack.allowedToBoost)
            {
                jetpack.allowedToBoost = true;
            }
            jetpack.RefillJetpackFuel();
        }
    }

    private bool PlayerInput(KeyCode input)
    {
        return Input.GetKey(input);
    }

    private void Death()
    {
        SceneManager.LoadScene(0);
        hp = 20;
    }
}