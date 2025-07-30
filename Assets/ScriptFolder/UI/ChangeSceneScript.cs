using UnityEngine;

public class ChangeSceneScript : MonoBehaviour
{
    public string sceneName;
    GameObject GameManagerObj;
    SceneController sceneController;
    public string playerTag;
    public float positionX;
    public float positionY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        sceneController = GameManagerObj.GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            Vector3 position = new Vector3(positionX,positionY,0.0f);
            SaveSystem.SavePlayerPosition(position);
            sceneController.changeScene(sceneName);
        }
    }
}
