using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

    public static void SavePlayerStage(int stage)
    {
        SaveFile saveFile = LoadPlayer();
        saveFile.stage = stage;
        WriteToFile(saveFile);
    }
}
