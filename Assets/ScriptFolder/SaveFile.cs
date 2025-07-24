using System;
using UnityEngine;

[Serializable]
public class SaveFile
{
    public float[] position;
    public int health;

    public SaveFile(PlayerControllerScript player)
    {
        // Store player's position
        Vector3 pos = player.transform.position;
        position = new float[] { pos.x, pos.y, pos.z };

        // Store player's health (assumes public int health field)
        health = player.Health;
    }
}
