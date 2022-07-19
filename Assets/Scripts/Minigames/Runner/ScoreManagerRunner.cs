using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerRunner : MonoBehaviour
{
    public float score;
    public Text scoreDisplay;
    public GameObject gameOver;

    void Update() {
        scoreDisplay.text = "Pontos: " + score.ToString(); 
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("ObstacleRunner") && !gameOver.activeSelf)
            score += 0.5f;
        Destroy(other.gameObject);
    }
}