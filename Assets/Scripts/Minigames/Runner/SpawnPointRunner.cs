using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointRunner : MonoBehaviour
{
    public GameObject obstacle;

    void Start() {
        Instantiate(obstacle, transform.position, Quaternion.identity); 
        Destroy(gameObject,.3f);   
    }
}
