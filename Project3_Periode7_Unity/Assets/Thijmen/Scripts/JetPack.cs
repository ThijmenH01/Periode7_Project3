using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    private Rigidbody playerRB;
    private PlayerScript playerScript;
    private RectTransform bar;

    public bool allowedToBoost;

    [Header("FUEL VALUES")]
    [SerializeField] private float fuelCapactity;

    public float currentFuel;

    [Header("SPEED VALUES")]
    [SerializeField] private float jetpackForceVertical;

    [SerializeField] private float jetpackForceHorizontal;

    [Range(0, 500)]
    [SerializeField] private float fuelUssageSpeed;

    [Range(0, 500)]
    [SerializeField] private float fuelRefillSpeed = 100f;

    private bool fueltankEmpty;

    private void Start()
    {
        playerScript = GetComponentInParent<PlayerScript>();
        playerRB = GetComponentInParent<Rigidbody>();
        currentFuel = fuelCapactity;
        bar = GameObject.Find("FuelBar").GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (PlayerInput(KeyCode.Space))
        {
            UseJetPackFuel();
        }

        if (currentFuel >= 0)
        {
            fueltankEmpty = false;
        }

        bar.localScale = new Vector3(currentFuel / 100, 1, 1);
    }

    public void UseJetPackFuel()
    {
        if (currentFuel > 0)
        {
            currentFuel -= fuelUssageSpeed * Time.deltaTime;

            if (currentFuel <= 0)
            {
                fueltankEmpty = true;
            }
        }
    }

    public void RefillJetpackFuel()
    {
        if (currentFuel <= fuelCapactity)
        {
            currentFuel += fuelRefillSpeed * Time.deltaTime;
            fueltankEmpty = false;
        }
    }

    public void JetPackMovement()
    {
        float dir = 0;
        if (!fueltankEmpty)
        {
            if (playerScript.playerDir == PlayerScript.PlayerDir.Left)
            {
                dir = -jetpackForceHorizontal;
            }
            if (playerScript.playerDir == PlayerScript.PlayerDir.Right)
            {
                dir = jetpackForceHorizontal;
            }
            playerRB.AddForce(new Vector3(dir, jetpackForceVertical, Time.deltaTime * 10), ForceMode.Force);
        }
    }

    public IEnumerator DragAsync()
    {
        allowedToBoost = false;
        playerRB.drag = 0.1f;
        playerRB.mass = 0.1f;
        print(playerRB.mass);
        yield return new WaitForSeconds(0.075f);
        playerRB.drag = 1f;
        playerRB.mass = 1f;
        print(playerRB.mass);
    }

    private bool PlayerInput(KeyCode input)
    {
        return Input.GetKey(input);
    }
}