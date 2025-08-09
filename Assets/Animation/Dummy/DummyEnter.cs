using UnityEngine;

public class DummyEnter : MonoBehaviour
{
    public SpriteRenderer interactKey;
    SceneController sceneController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SceneController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        interactKey.enabled = true;
        if (Input.GetKeyDown(KeyCode.E))
        {
            sceneController.changeScene("SceneRangeDummy");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        interactKey.enabled = true;
        if (Input.GetKeyDown(KeyCode.E))
        {
            sceneController.changeScene("SceneRangeDummy");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        interactKey.enabled = false;
    }

}
