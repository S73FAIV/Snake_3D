using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenu : MonoBehaviour
{
    public List<Text> HighScoreTexts;

    void Start()
    {
        for (int i = 0; i < HighScoreTexts.Count; i++)
        {
            int score = PlayerPrefs.GetInt(String.Format("HighScore#{0}", i));
            string player = PlayerPrefs.GetString(String.Format("PlayerScore#{0}", i), "--");

            HighScoreTexts[i].text = String.Format("#{0}: \t {1:00000}: \t {2}", i + 1, score, player);
        }
    }
}