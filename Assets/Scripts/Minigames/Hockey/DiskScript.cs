using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskScript : MonoBehaviour
{
    public ScoreScript ScoreScriptInstance;
    public static bool WasGoal { get; private set;}
    public float MaxSpeed;

    public AudioManager audioManager;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WasGoal = false;    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!WasGoal)
        {
            if (other.tag == "P2Goal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.P1Score);
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetDisk(false));
            }
            else if (other.tag == "P1Goal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.P2Score);
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetDisk(true));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.PlayDiskCollision();
    }

    private IEnumerator ResetDisk(bool didP2Score)
    {
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        rb.velocity = rb.position = new Vector2(0,0);

        if (didP2Score)
            rb.position = new Vector2(-1,0);
        else
            rb.position = new Vector2(1,0);
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
    }
}