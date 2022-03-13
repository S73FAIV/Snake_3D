using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = System.Random;

public class Utility
{
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        Random prng = new Random(seed);

        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            (array[randomIndex], array[i]) = (array[i], array[randomIndex]);
        }

        return array;
    }

    public static int Mod(int a, int n) => (a % n + n) % n;

    public static Dictionary<int, string> GetSavedHighScoresDictionary()
    {
        Dictionary<int, string> currentHighScores = new Dictionary<int, string>();
        int i = 0;
        while (PlayerPrefs.GetInt(String.Format("HighScore#{0}", i)) != 0)
        {
            i++;
            int score = PlayerPrefs.GetInt(String.Format("HighScore#{0}", i));
            string player = PlayerPrefs.GetString(String.Format("PlayerScore#{0}", i));
            currentHighScores.Add(score, player);
        }

        return currentHighScores;
    }
    
    public static List<string> GetSavedHighScorePlayers()
    {
        List<string> highScorePlayers = new List<string>();
        int i = 0;
        while (PlayerPrefs.GetInt(String.Format("HighScore#{0}", i)) != 0)
        {
            i++;
            string player = PlayerPrefs.GetString(String.Format("PlayerScore#{0}", i));
            highScorePlayers.Add(player);
        }

        return highScorePlayers;
    }
    
    public static List<int> GetSavedHighScores()
    {
        List<int> currentHighScores = new List<int>();
        int i = 0;
        while (PlayerPrefs.GetInt(String.Format("HighScore#{0}", i)) != 0)
        {
            i++;
            int score = PlayerPrefs.GetInt(String.Format("HighScore#{0}", i));
            currentHighScores.Add(score);
        }

        return currentHighScores;
    }
}