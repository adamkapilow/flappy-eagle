using System.Collections;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class ScoreData
{
    // Stores high score data for the various difficulty settings.
    public int[] highScore;

    public ScoreData()
    {
        highScore = GameManager.Instance.HighScore;
    }
}
