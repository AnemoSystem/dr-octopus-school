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

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("eae");
            // Player takes damage
            other.GetComponent<PlayerRunner>().health -= damage;
            Destroy(gameObject);
        }
    }
}
