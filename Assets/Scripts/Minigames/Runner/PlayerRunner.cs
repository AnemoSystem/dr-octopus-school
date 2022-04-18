using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRunner : MonoBehaviour
{
    private Vector2 targetPos;
    public float Yincrement;
    public float speed;
    public float maxHeight;
    public float minHeight;
    public int health;
    public Text healthDisplay;
    public GameObject gameOver;
    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        healthDisplay.text = "Vida: " + health.ToString();
        if (health <= 0) {
            anim.SetBool("death", true);
            gameOver.SetActive(true);
            //Destroy(gameObject);
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if (transform.position.y == 0 || transform.position.y == maxHeight || transform.position.y == minHeight) {
                if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < maxHeight) {
                    targetPos = new Vector2(transform.position.x, transform.position.y + Yincrement);
                } else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > minHeight) {
                    targetPos = new Vector2(transform.position.x, transform.position.y - Yincrement);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("ObstacleRunner") && health > 0) {
            // Player takes damage
            anim.SetTrigger("damage");
            other.GetComponent<Animator>().SetTrigger("death");
            health -= 1;
            //Destroy(other.gameObject);
        }
    }
}
