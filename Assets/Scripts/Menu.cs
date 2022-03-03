using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject mainMenuHolder;
    public GameObject optionsMenuHolder;

    public int[] mapSizes;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Mapsize") == 0)
        {
            PlayerPrefs.SetInt("Mapsize", mapSizes[0]);
        }

        if (PlayerPrefs.GetFloat("speedMultiplier") == 0)
        {
            PlayerPrefs.SetFloat("speedMultiplier", 1);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        mainMenuHolder.SetActive(false);
        optionsMenuHolder.SetActive(true);
    }

    public void MainMenu()
    {
        mainMenuHolder.SetActive(true);
        optionsMenuHolder.SetActive(false);
    }

    public void SetMapSize(int i)
    {
        PlayerPrefs.SetInt("Mapsize", mapSizes[i]);
    }

    public void SetSpeed(float i)
    {
        PlayerPrefs.SetFloat("speedMultiplier", (float) Math.Round(i, 2));
    }

    public void ShowCurrentValue(float speed)
    {
        List<Text> texts = new List<Text>(GameObject.FindObjectsOfType<Text>());
        Text text = texts.Find(text => text.name == "SliderValueText");
        speed = (float) Math.Round(speed, 2);
        if (text != null)
        {
            text.text = speed + "x";
        }
    }

}