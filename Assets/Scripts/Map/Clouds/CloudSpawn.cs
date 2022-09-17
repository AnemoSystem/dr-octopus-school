using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : MonoBehaviour
{
    public GameObject Cloud1;
    public GameObject Cloud2;
 
    public float delay;
 
    public static bool spawnClouds = true;
 
    void Start () {
        StartCoroutine(SpawnClouds());
    }
 
    IEnumerator SpawnClouds() {
        while(true) {
            while(spawnClouds) {
                Instantiate(Cloud1);
                yield return new WaitForSeconds(delay);
                Instantiate(Cloud2);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
