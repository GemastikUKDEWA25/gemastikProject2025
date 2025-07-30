using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SavingTree : MonoBehaviour
{
    public TextMeshProUGUI InteractKey;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerControllerScript player = collision.GetComponent<PlayerControllerScript>();
            InteractKey.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                // SaveSystem.SavePlayerPosition(player.transform.position);

                SaveSystem.SavePlayerStage("SceneTest1");
                SaveSystem.SavePlayerPosition(player.transform.position);

                SaveFile data = SaveSystem.LoadPlayer();


                SaveSystem.SaveStagePosition(data.stage,data.position);
                Debug.Log(data.stageKeys.ToString());
                Debug.Log(data.stagePositions.ToString().ToString());
                


                Debug.Log("Saving");
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerControllerScript player = collision.GetComponent<PlayerControllerScript>();
            InteractKey.enabled = true;
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                string sceneName = SceneManager.GetActiveScene().name;
                // SaveSystem.SavePlayerPosition(player.transform.position);
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

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InteractKey.enabled = false;            
        }
    }
}
