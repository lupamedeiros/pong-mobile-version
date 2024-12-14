using UnityEngine;
using UnityEngine.UI;

public class PongScoreManager : MonoBehaviour
{
    private int leftScore = 0;
    private int rightScore = 0;
    public Text scoreText;
    public void ScorePoint(string side)
    {
        if (side == "Left")
            leftScore++;
        else if (side == "Right")
            rightScore++;

        scoreText.text = $"Left: {leftScore} - Right: {rightScore}";
    }

    public int GetScore(string side)
    {
        if (side == "Left")
            return leftScore;
        else if (side == "Right")
            return rightScore;

        return 0;
    }

    public void ResetScore()
    {
        leftScore = 0;
        rightScore = 0;
    }
}