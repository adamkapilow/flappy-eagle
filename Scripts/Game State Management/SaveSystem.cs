using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    // Save and load system for score data. Using binary encoding and decoding
    // as a modest security feature.
   public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "score_data.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        ScoreData data = new ScoreData();

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static ScoreData Load()
    {
        string path = Application.persistentDataPath + "score_data.fun";
        if (!File.Exists(path))
        {
            Debug.LogError("No file found");
            return null;
        }

        FileStream stream = new FileStream(path, FileMode.Open);

        BinaryFormatter formatter = new BinaryFormatter();

        ScoreData data = formatter.Deserialize(stream) as ScoreData;
        stream.Close();
        return data;

    }
}
