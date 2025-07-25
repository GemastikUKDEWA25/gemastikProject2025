using System;
using UnityEngine;

[Serializable]
public class SaveFile
{
    public float[] position;
    public int health;
    public int stage;
    public string name; // Add name field

    public SaveFile() { } // Needed for creating empty or default save

    public SaveFile(PlayerControllerScript player)
    {
        Vector3 pos = player.transform.position;
        position = new float[] { pos.x, pos.y, pos.z };
        health = player.Health;
        stage = player.stage;
        name = player.playerName; // assumes playerName is public string
    }
}
