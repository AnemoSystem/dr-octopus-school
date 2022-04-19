using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRunner : MonoBehaviour
{
    public int damage = 1;
    public float speed;

    void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
