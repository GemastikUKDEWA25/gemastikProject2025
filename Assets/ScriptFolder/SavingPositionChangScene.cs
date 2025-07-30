using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingPositionChangScene : MonoBehaviour
{
    Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (collision.CompareTag("Player"))
        {
            SaveSystem.SavePlayerStage(sceneName);
            SaveSystem.SavePlayerPosition(player.transform.position);
            SaveFile data = SaveSystem.LoadPlayer();
            SaveSystem.SaveStagePosition(data.stage, data.position);
            Debug.Log(data.stageKeys.ToString());
            Debug.Log(data.stagePositions.ToString().ToString());

            Debug.Log("Saving");
        }
    }
}
