using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public string playerNick = "---";
    private bool gameOver = false;

    public Text scoreText;
    public Text gameOverScore;

    private float _msBetweenScoreIncrement = 300;
    private float _nextScoreIncTime;

    private int scoreIncrementTime = 10;
    private int scoreIncrementEat = 1000;

    void Start()
    {
        FindObjectOfType<SnakeController>().OnEat += OnEat;
        FindObjectOfType<SnakeController>().OnDeath += OnDeath;
        InitScoreIncrements();
    }

    void Update()
    {
        scoreText.text = string.Format("Score: {0:00000}", score);
    }

    private void FixedUpdate()
    {
        if (Time.time > _nextScoreIncTime && !gameOver)
        {
            _nextScoreIncTime = Time.time + _msBetweenScoreIncrement / 1000;
            score += scoreIncrementTime / Mathf.RoundToInt(Mathf.Log(score));
        }
    }

    private void InitScoreIncrements()
    {
        float speed = PlayerPrefs.GetFloat("speedMultiplier");
        if (speed == 0)
        {
            _msBetweenScoreIncrement = 300;
        }
        else
        {
            _msBetweenScoreIncrement = 300 / speed;
        }
    }

    private void OnEat()
    {
        score += scoreIncrementEat;
    }

    private void OnDeath()
    {
        gameOver = true;
        scoreText.enabled = false;
        gameOverScore.text = string.Format("You: {0:00000}", score);
    }

    public void SetPlayerNick(string _playerNick)
    {
        playerNick = _playerNick;
    }
}