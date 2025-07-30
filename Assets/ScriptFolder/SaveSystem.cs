using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/player.fun";

    public static void SavePlayer(PlayerControllerScript player)
    {
        SaveFile saveFile = new SaveFile(player);
        WriteToFile(saveFile);
    }

    public static SaveFile LoadPlayer()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveFile saveFile = formatter.Deserialize(stream) as SaveFile;
            stream.Close();
            return saveFile;
        }
        else
        {
            Debug.LogWarning("Save File not found at " + path);
            return new SaveFile(); // return empty object to avoid null
        }
    }

    private static void WriteToFile(SaveFile saveFile)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, saveFile);
        stream.Close();
    }

    // --- Separate functions ---

    public static void SavePlayerPosition(Vector3 position)
    {
        SaveFile saveFile = LoadPlayer();
        saveFile.position = new float[] { position.x, position.y, position.z };
        WriteToFile(saveFile);
    }

    public static void SavePlayerName(string name)
    {
        SaveFile saveFile = LoadPlayer();
        saveFile.name = name;
        WriteToFile(saveFile);
    }

    public static void SavePlayerStage(string stage)
    {
        SaveFile saveFile = LoadPlayer();
        saveFile.stage = stage;
        WriteToFile(saveFile);
    }

    public static void SaveStagePosition(string stageName, float[] stagePosition)
    {
        SaveFile saveFile = LoadPlayer();
        if (saveFile.stageKeys == null) saveFile.stageKeys = new List<string>();
        if (saveFile.stagePositions == null) saveFile.stagePositions = new List<float[]>();

        if (saveFile.stageKeys != null && saveFile.stagePositions != null && saveFile.stageKeys.Count == saveFile.stagePositions.Count)
        {
            bool Exist = false;
            for (int i = 0; i < saveFile.stageKeys.Count; i++)
            {
                if (saveFile.stageKeys[i] == stageName)
                {
                    saveFile.stagePositions[i] = stagePosition;
                    Exist = true;
                    break;
                }
            }

            if (!Exist)
            {
                saveFile.stageKeys.Add(stageName);
                saveFile.stagePositions.Add(stagePosition);
            }
        }

        WriteToFile(saveFile);
    }

    public static void DeleteSaveFile()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save file deleted.");
        }
        else
        {
            Debug.LogWarning("No save file to delete at: " + path);
        }
    }
}
