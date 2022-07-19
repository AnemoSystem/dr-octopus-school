using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRunner : MonoBehaviour
{
    public GameObject[] obstaclePatterns;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float decreaseTime;
    public float minTime = 0.65f;

    private float delete = 1;
    private GameObject reference;

    void Update() {
        if (timeBtwSpawn <= 0) {
            int rand = Random.Range(0, obstaclePatterns.Length);
            reference = Instantiate(obstaclePatterns[rand], transform.position, Quaternion.identity);
            timeBtwSpawn = startTimeBtwSpawn;
            if (startTimeBtwSpawn > minTime) {
                startTimeBtwSpawn -= decreaseTime;
            }
        } else {
            timeBtwSpawn -= Time.deltaTime;
            delete -= Time.deltaTime;

            if(delete < 0) {
                Destroy(reference);
                delete = 1;
            }
        }
    }
}
