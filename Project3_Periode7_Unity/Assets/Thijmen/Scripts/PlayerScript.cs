using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float rotSpeed;
    public PlayerDir playerDir;
    private JetPack jetpack;
    private Rigidbody rb;
    private float rotValue;
    public static PlayerScript instance;

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
    }

    private void Update()
    {
        rotValue = Mathf.Clamp(rb.velocity.magnitude * 10, 0, 40);
        EnumBehaviour();

        if (PlayerInput(KeyCode.Space))
        {
            jetpack.UseJetPackFuel();

            if (jetpack.allowedToBoost)
            {
                StartCoroutine(jetpack.DragAsync());
            }
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
    }

    private void EnumBehaviour()
    {
        if (PlayerInput(KeyCode.A))
        {
            playerDir = PlayerDir.Left;
        }
        else if (PlayerInput(KeyCode.D))
        {
            playerDir = PlayerDir.Right;
        }
        else
        {
            playerDir = PlayerDir.Neutral;
        }
    }

    private void FixedUpdate()
    {
        if (PlayerInput(KeyCode.Space))
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
}