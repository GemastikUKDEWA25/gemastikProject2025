using UnityEditor.SearchService;
using UnityEngine;

public class ReforestationScript : MonoBehaviour
{
    public GameObject treesParent;
    SceneController sceneController;
    public int treesCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SceneController>();
        treesCount = treesParent.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        // int totalChildren = GetTotalDescendants(treesParent.transform);
        // Debug.Log("Total descendants: " + totalChildren);
        int childCount = treesParent.transform.childCount;
        Debug.Log(childCount);
        if (treesCount <= 0)
        {
            sceneController.changeScene("SceneReforestation");
        }
    }
}
