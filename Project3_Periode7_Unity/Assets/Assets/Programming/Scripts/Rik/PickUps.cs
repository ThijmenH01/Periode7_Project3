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
        }
    }
}