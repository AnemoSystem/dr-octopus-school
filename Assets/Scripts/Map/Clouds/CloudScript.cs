using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
 
    public float minY;
    public float maxY;
    
    public float buffer;
 
    float speed;
    float camWidth;
 
    void Start() {
        camWidth = Camera.main.orthographicSize * Camera.main.aspect;
 
        speed = Random.Range(minSpeed, maxSpeed);
        transform.position = new Vector3(-camWidth -8 - buffer, Random.Range(minY, maxY), transform.position.z);
    }

    void Update() {

        transform.Translate(speed * Time.deltaTime, 0, 0);
        if(transform.position.x - buffer > camWidth + 8) {
            Destroy(gameObject);
        }
    }
}
