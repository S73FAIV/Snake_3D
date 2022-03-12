using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score;
    public Text scoreText;

    private float msBetweenScoreIncrement = 300;
    private float _nextScoreIncTime;

    private int scoreIncrementTime = 10;
    private int scoreIncrementEat = 1000;

    void Start()
    {
        FindObjectOfType<SnakeController>().OnEat += OnEat;
        InitScoreIncrements();
    }

    void Update()
    {
        scoreText.text = string.Format("Score: {0:00000}", score);
    }

    private void FixedUpdate()
    {
        if (Time.time > _nextScoreIncTime)
        {
            _nextScoreIncTime = Time.time + msBetweenScoreIncrement / 1000;
            score += scoreIncrementTime;
        }
    }

    private void InitScoreIncrements()
    {
        float speed = PlayerPrefs.GetFloat("speedMultiplier");
        if (speed == 0)
        {
            msBetweenScoreIncrement = 300;
        }

        else
        {
            msBetweenScoreIncrement = 300 / speed;
        }
    }

    private void OnEat()
    {
        score += scoreIncrementEat;
        Debug.Log("New EatScore: " + score);
    }
}