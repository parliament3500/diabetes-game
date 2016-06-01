using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public static float timeElapsed;
    public Text scoreText;
    public Text highscoreText;
    public static float bestTime = 0f;
    public static bool newHighscore;

    void Start()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime");
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            timeElapsed += Time.deltaTime;
        }
        //check if you beat the highscore
        if (timeElapsed > bestTime)
        { 
            bestTime = timeElapsed;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            newHighscore = true;
        }

        string textColor = newHighscore ? "#FF0" : "#FFF";

        scoreText.text = "TIME: " + FormatTime(timeElapsed);  //http://docs.unity3d.com/Manual/StyledText.html
        highscoreText.text = "<color=" + textColor + ">BEST: " + FormatTime(bestTime) + "</color>";

    }

    string FormatTime(float value)
    {
        TimeSpan t = TimeSpan.FromSeconds(value); //converts floats to seconds
        //format the text to minutes and seconds
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);   
    }
}

