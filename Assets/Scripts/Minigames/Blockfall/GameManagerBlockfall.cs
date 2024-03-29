﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerBlockfall : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;
    //public Brick[] bricks { get; private set; }
    private Brick[] bricks;
    public Text scoreDisplay;
    public Text livesDisplay;
    public Text levelDisplay;
    public GameObject gameOver;
    public LoadWithTransition transition;

    public GameObject ballObject;
    public GameObject paddleObject;
    public GameObject bricksReference;
    public GameObject menuStart;
    public GameObject menuScore;

    //const int NUM_LEVELS = 2;

    private int level = 1;
    public int score = 0;
    public int lives = 3;

    public void NewGame() {
        ballObject.SetActive(true);
        paddleObject.SetActive(true);
        bricksReference.SetActive(true);
        menuStart.SetActive(false);
        menuScore.SetActive(true);
    }

/*
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }
*/
    private void Start()
    {
        levelDisplay.text = level.ToString();
        ResetData();
        //ball = FindObjectOfType<Ball>();
        //paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>(true);
    }

    private void ResetData()
    {
        score = 0;
        lives = 3;

        //LoadLevel(1);
    }

    void Update() {
        scoreDisplay.text = score.ToString();
        livesDisplay.text = lives.ToString(); 
    }

    //private void LoadLevel(int level)
    //{
        //this.level = level;

        //if (level > NUM_LEVELS)
        //{
            // Start over again at level 1 once you have beaten all the levels
            // You can also load a "Win" scene instead
            //LoadLevel(1);
            //return;
        //}

        //SceneManager.LoadScene("Level" + level);
    //}
/*
    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
    }
*/
    public void Miss()
    {
        lives--;

        if (lives > 0) {
            ResetLevel();
        } else {
            //GameOver();
            gameOver.SetActive(true);
            
        }
    }

    private void ResetLevel()
    {
        paddle.ResetPaddle();
        ball.ResetBall();

        // Resetting the bricks is optional
        // for (int i = 0; i < bricks.Length; i++) {
        //     bricks[i].ResetBrick();
        // }
    }

    private void ResetAllLevel()
    {
        paddle.ResetPaddle();
        ball.ResetBall();

        for (int i = 0; i < bricks.Length; i++) {
            bricks[i].ResetBrick();
        }
    }    

    private void GameOver()
    {
        // Start a new game immediately
        // You can also load a "GameOver" scene instead
        NewGame();
    }

    public void Hit(Brick brick)
    {
        score += brick.points;

        if(score % 500 == 0)
            Server.bonusCoins += 1;
        
        if(Cleared()) {
            //transition.FadeIn("BlockFall");
            ResetAllLevel();
            level++;
            ball.SetSpeed(ball.GetSpeed() * 1.2f);
            levelDisplay.text = level.ToString();    
        }
        
        /*
        if (Cleared()) {
            SceneManager.LoadScene("Blockfall");
            NewGame();
            ball.SetSpeed(ball.GetSpeed() * 1.2f);
        }
        */
    }

    private bool Cleared()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i].gameObject.activeSelf && !bricks[i].unbreakable) {
                return false;
            }
        }

        return true;
    }

}