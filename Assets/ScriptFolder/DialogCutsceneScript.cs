using UnityEngine;
using TMPro;
using System.Collections;

[System.Serializable]
public class DialogCutscene
{
    public string name;
    public string dialog;
    public Sprite expression;
}

public class DialogCutsceneScript : MonoBehaviour
{
    // public string[] dialog;
    public UnityEngine.Playables.PlayableDirector director;
    bool isInDialog = false;
    int dialogCounter = 0;
    bool isInInteractArea = false;
    float wordDelay = 0.09f;
    TextMeshProUGUI dialogtext;
    TextMeshProUGUI dialogName;

    UnityEngine.UI.Image dialogBackground;
    UnityEngine.UI.Image characterExpression;

    double pauseTime;
    public GameObject dialogPosition;
    public GameObject character;
    
    public AudioClip clip;
    public AudioSource audioSource;
    PlayerControllerScript playerController;
    public DialogCutscene[] dialogList;
    void Start()
    {
        dialogBackground = GameObject.Find("DialogBackground").GetComponent<UnityEngine.UI.Image>();

        characterExpression = GameObject.Find("ExpressionDialog").GetComponent<UnityEngine.UI.Image>();

        dialogtext = GameObject.Find("DialogTMP").GetComponent<TextMeshProUGUI>();
        dialogName = GameObject.Find("SpeakerName").GetComponent<TextMeshProUGUI>();
        
        
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInDialog)
        {
            director.time = pauseTime;
            director.Evaluate(); // Apply the state at this time
        }
        // showDialog();
        if (Input.GetKeyDown(KeyCode.Space) && isInDialog == true)
        {
            dialogCounter += 1;
            dialogtext.text = "";
            dialogName.text = "";
            if (dialogCounter < dialogList.Length)
            {
                showDialog();
            }
        }

        if (dialogCounter >= dialogList.Length)
        {
            hideDialog();
        }
    }

    public void ShowDialogEvent()
    {
        if (!isInDialog)
        {
            if (director != null)
            {
                // Freeze the timeline at the current time when the signal triggers
                pauseTime = director.time;
                director.time = pauseTime;
                director.Evaluate(); // Apply the state at this time
            }

            dialogCounter = 0;
            showDialog(); // your own dialogue logic
            isInDialog = true;
        }
    }
    void showDialog()
    {   
        if (playerController != null) playerController.setIsInDialog(true);
            isInDialog = true;
        dialogBackground.enabled = true;
        characterExpression.enabled = true;
        string[] dialogSplit = dialogList[dialogCounter].dialog.Split(" ");
        dialogName.text = dialogList[dialogCounter].name;
        if (dialogList[dialogCounter].expression != null) characterExpression.sprite = dialogList[dialogCounter].expression;
        StartCoroutine(TypeText(dialogSplit));
    }

    void hideDialog()
    {
        isInDialog = false; 
        if (playerController != null) playerController.setIsInDialog(false);
        dialogCounter = 0;
        characterExpression.enabled = false;
        dialogBackground.enabled = false;
        dialogtext.text = "";
    }

    IEnumerator TypeText(string[] dialogSplit)
    {       
        foreach (string word in dialogSplit)
        {
            if (isInDialog)
            {                
                dialogtext.text += word + " ";
                audioSource.PlayOneShot(clip);
                yield return new WaitForSeconds(wordDelay);
            }
        }
    }
}
