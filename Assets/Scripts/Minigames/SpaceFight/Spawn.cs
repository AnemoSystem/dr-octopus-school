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
        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        
        for(int i=0; i < waves; i++)
            Instantiate(enemies[(int)Random.Range(0,enemies.Length)],new Vector3(spawnX, Random.Range(7f,30f) ,0),Quaternion.identity);
    }
}
