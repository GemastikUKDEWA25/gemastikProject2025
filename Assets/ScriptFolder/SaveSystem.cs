using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerControllerScript player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveFile saveFile = new SaveFile(player);
        formatter.Serialize(stream, saveFile);
        stream.Close();
    }

    public static SaveFile LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
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
            return null;
        }
    }
}
