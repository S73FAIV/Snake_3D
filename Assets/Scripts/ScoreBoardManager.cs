using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreBoardManager : MonoBehaviour
{
    public List<Text> HighScoresTexts;

    private List<int> savedHighScores;
    private List<string> savedHighScorePlayers;
    private ScoreManager manager;

    void Start()
    {
        savedHighScores = Utility.GetSavedHighScores();
        savedHighScorePlayers = Utility.GetSavedHighScorePlayers();
        if (SceneManager.GetActiveScene().name == "Game")
        {
            manager = GetComponent<ScoreManager>();
            manager.ScoreFinished += SaveNewScore;
        }
        PrintScoreBoard();
    }

    private void PrintScoreBoard()
    {
        for (int i = 0; i < HighScoresTexts.Count; i++)
        {
            try
            {
                int score = savedHighScores[i];
                string player = savedHighScorePlayers[i];
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

    private void SaveNewScore()
    {
        if (savedHighScores.Min() < manager.score)
        {
            
        }
    }
}