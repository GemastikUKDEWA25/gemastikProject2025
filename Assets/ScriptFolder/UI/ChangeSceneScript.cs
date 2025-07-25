using UnityEngine;

public class ChangeSceneScript : MonoBehaviour
{
    public string sceneName;
    GameObject GameManagerObj;
    SceneController sceneController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManagerObj = GameObject.Find("Game Manager");
        sceneController = GameManagerObj.GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sceneController.changeScene(sceneName);
        }
    }
}
