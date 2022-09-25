using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingbarrelspawner : MonoBehaviour
{
    public GameObject barrel;
    public float minTime = 5f;
    public float maxTime = 9f;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        Instantiate(barrel,transform.position,Quaternion.identity);
        Invoke(nameof(Spawn),Random.Range(minTime,maxTime));
    }

}
