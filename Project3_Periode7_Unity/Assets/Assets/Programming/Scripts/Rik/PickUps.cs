using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        transform.Rotate(0, 1, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && CompareTag("SMG"))
        {
            Destroy(gameObject);
            PlayerScript.instance.gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
            PlayerScript.instance.gameObject.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
        }
        if (collision.collider.CompareTag("Player") && CompareTag("Gun"))
        {
            Destroy(gameObject);
            PlayerScript.instance.gameObject.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
            PlayerScript.instance.gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
        }

        if (collision.collider.CompareTag("Player") && CompareTag("Medkit"))
        {
            Destroy(gameObject);
            PlayerScript.hp += 10;
        }
    }
}