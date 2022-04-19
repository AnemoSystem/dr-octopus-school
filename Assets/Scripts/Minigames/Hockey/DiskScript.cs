using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskScript : MonoBehaviour
{
    public ScoreScript ScoreScriptInstance;
    public static bool WasGoal { get; private set;}
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
                StartCoroutine(ResetDisk());
            }
            else if (other.tag == "P1Goal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.P2Score);
                WasGoal = true;
                StartCoroutine(ResetDisk());
            }
        }
    }

    private IEnumerator ResetDisk()
    {
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        rb.velocity = rb.position = new Vector2(0,0);
    }
}