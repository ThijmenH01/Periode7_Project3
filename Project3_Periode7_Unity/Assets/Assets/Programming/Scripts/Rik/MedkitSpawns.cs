using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitSpawns : MonoBehaviour
{
    [SerializeField] private GameObject medkit;
    [SerializeField] private GameObject smg;
    [SerializeField] private GameObject rifle;
    public static MedkitSpawns Instance;

    private void Start()
    {
        Instance = this;
    }

    public void SpawnAtRandom()
    {
        int randomPoint = Random.Range(1, 6);
        Instantiate(medkit, transform.GetChild(randomPoint).transform.position, Quaternion.identity);
        int randomPoint2 = Random.Range(1, 6);

        int randomGun = Random.Range(1, 3);
        if (randomGun == 1)
        {
            Instantiate(rifle, transform.GetChild(randomPoint2).transform.position, Quaternion.identity);
        }
        if (randomGun == 2)
        {
            Instantiate(smg, transform.GetChild(randomPoint2).transform.position, Quaternion.identity);
        }
    }
}