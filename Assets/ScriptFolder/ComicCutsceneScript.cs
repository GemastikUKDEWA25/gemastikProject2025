using UnityEngine;
using UnityEngine.Video;
using TMPro;
using System.Collections;

[System.Serializable]
public class cutSceneNaration
{
    public VideoClip videoScene;
    public SpriteRenderer comicPanel;
    public string textNarator;
} 
public class ComicCutsceneScript : MonoBehaviour
{
    public cutSceneNaration[] sceneNarations;
    public VideoPlayer videoPlayer;
    public TextMeshProUGUI textNarator;
    public SceneController sceneController;
    public float wordDelay;
    public float sceneDelay;
    public string nextScene;
    bool controlTrigger = false;
    private int naratorCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SceneController>();
        nextPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (naratorCounter > sceneNarations.Length)
        {
            sceneController.changeScene(nextScene);    
        }

        if (sceneDelay > 0) sceneDelay -= Time.deltaTime;
        if (sceneDelay <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nextPanel();
            }
        }
        
    }

    void nextPanel()
    {
        if (naratorCounter < sceneNarations.Length)
        {
            cutSceneNaration naration = sceneNarations[naratorCounter];
            if (naration.videoScene != null) videoPlayer.clip = naration.videoScene;
            textNarator.text = "";
            string[] text = naration.textNarator.Split(" ");
            StartCoroutine(TypeText(text)); 
        }
        naratorCounter += 1;
    }

    IEnumerator TypeText(string[] dialogSplit)
    {       
        foreach (string word in dialogSplit)
        {
            textNarator.text += word + " ";
            yield return new WaitForSeconds(wordDelay);
        }
        
    }
}
