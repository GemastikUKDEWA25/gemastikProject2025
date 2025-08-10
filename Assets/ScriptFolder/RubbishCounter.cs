using UnityEngine;

public class RubbishCounter : MonoBehaviour
{

    public static RubbishCounter instance;
    int inOrganic = 0;
    int organic = 0;
    int b3 = 0;
    public int target;
    public string changeSceneName;
    SceneController sceneController;
    bool isChange = false;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        sceneController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SceneController>();
    }

    void Update()
    {
        Debug.Log(inOrganic + organic + b3);
        if (inOrganic + organic + b3 >= target && !isChange)
        {
            sceneController.changeScene(changeSceneName);
            isChange = true;
        }
    }

   public void addInOrganic()
    {
        inOrganic += 1;
    }

    public void addOrganic()
    {
        organic += 1; 
    }

    public void addB3()
    {
        b3 += 1;       
    }

}
