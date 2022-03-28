using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedChanged : MonoBehaviour
{
    // Sets global game speed to user input level. Minimum 50%, max 200%

    public Text speedLabel;
    // Display for current speed level.

    private void Awake()
    {
        setText();
    }

    // Displays the current speed level
    public void setText()
    {
        int roundedSpeed = Mathf.RoundToInt(PlayerPrefs.GetFloat("time scale", 1) * 100);
        speedLabel.text = "Current speed:\n" + roundedSpeed + "%";
    }

    // Sets the current game speed to a given input percentage of default.
    // Capped at 50% to 200% speed. Defaults to 100% if input is not an integer.

    public void gameSpeedChanged(string speedString)
    {
        int speed;
        bool parsed = int.TryParse(speedString, out speed);
        if (!parsed)
        {
            Time.timeScale = 1;
            speedLabel.text = "100";
            return;
        }
        float newTime = speed / 100.0f;
        if (newTime <= 0.5)
        {
            Time.timeScale = 0.5f;
            speedLabel.text = "50";
        }
        else if (newTime >= 2)
        {
            Time.timeScale = 2;
            speedLabel.text = "200";
        }
        else
        {
            Time.timeScale = newTime;
        }
        
        PlayerPrefs.SetFloat("time scale", Time.timeScale);
        setText();
    }
}
