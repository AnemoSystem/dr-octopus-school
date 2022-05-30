using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int lives = 3;
    private void Start() 
    {
        NewGame();
    }
    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;
    }
}
