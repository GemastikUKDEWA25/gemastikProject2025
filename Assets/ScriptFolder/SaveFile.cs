using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveFile
{
    public int health;
    public string stage;
    public string name;
    public float[] position;
    public List<string> stageKeys;         // Example: "Stage_1", "Stage_2", ...
    public List<float[]> stagePositions;   // Example: [x, y, z]

    public SaveFile()
    {
        stageKeys = new List<string>();
        stagePositions = new List<float[]>();
    }

    public SaveFile(PlayerControllerScript player)
    {
        health = player.Health;
        stage = player.stage;
        name = player.playerName;

        stageKeys = new List<string>();
        stagePositions = new List<float[]>();

        // Just add the current stage and position
        stageKeys.Add(stage); // or just stage.ToString()
        Vector3 pos = player.transform.position;
        stagePositions.Add(new float[] { pos.x, pos.y, pos.z });
    }

}
