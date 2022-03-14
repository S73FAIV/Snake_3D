using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardManager : MonoBehaviour
{
    public List<Text> HighScoresTexts;

    public HighScore highScore;
    public ScoreManager manager;

    void Start()
    {
        highScore = Utility.LoadHighScore();
        PrintScoreBoard();
    }

    private void PrintScoreBoard()
    {
        for (int i = 0; i < HighScoresTexts.Count; i++)
        {
            try
            {
                int score = highScore.points[i];
                string player = highScore.players[i];
                HighScoresTexts[i].text = String.Format("#{0}: \t {1:00000}: \t {2}", i + 1, score, player);
            }
            catch (ArgumentOutOfRangeException)
            {
                if (i == 0)
                {
                    HighScoresTexts[i].text = "No Highscores";
                }
                else
                {
                    HighScoresTexts[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void SaveNewScore()
    {
        if (highScore.Min() < manager.score)
        {
            //get the place to insert the new score
            int length = highScore.points.Count;
            for (int i = 0; i < length; i++)
            {
                if (manager.score > highScore.points[i])
                {
                    highScore.Insert(i, manager.score, manager.playerNick);
                    highScore.Cleanup();
                }
            }
        }

        if (highScore.Count() == 0)
        {
            Debug.Log("savedHighScores empty");
            highScore.Insert(0, manager.score, manager.playerNick);
        }

        Utility.SaveHighScores(highScore);
    }
}

[Serializable]
public class HighScore
{
    public List<int> points = new List<int>();
    public List<string> players = new List<string>();

    public void Insert(int index, int highScore, string player)
    {
        points.Insert(index, highScore);
        players.Insert(index, player);
    }

    public int Count()
    {
        return points.Count;
    }

    public void Cleanup()
    {
        if (points.Count > 5)
        {
            points.RemoveAt(points.Count - 1);
            players.RemoveAt(points.Count - 1);
        }
    }

    public int Min()
    {
        int min = Int32.MaxValue;
        foreach (int number in points)
        {
            if (number < min)
            {
                min = number;
            }
        }
        return min;
    }
}