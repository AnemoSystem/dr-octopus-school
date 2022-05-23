using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public float rate;
    public GameObject[] enemies;
    public int waves = 1;
    void Start()
    {
        InvokeRepeating("Spawner",rate,rate);
         
    }

    void Spawner ( )
    {
        for(int i=0; i < waves; i++)
            Instantiate(enemies[(int)Random.Range(0,enemies.Length)],new Vector3(Random.Range(-8.5f,8.5f),7,0),Quaternion.identity);
    }
}
