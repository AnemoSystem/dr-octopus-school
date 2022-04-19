using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public enum Score
    {
        P2Score, P1Score
    }

    public Text P2ScoreTxt, P1ScoreTxt;
    private int p2Score, p1Score;
    
    public void Increment(Score whichScore)
    {
        if (whichScore == Score.P2Score)
            P2ScoreTxt.text = (++p2Score).ToString();
        else
            P1ScoreTxt.text = (++p1Score).ToString();
    }

    }
