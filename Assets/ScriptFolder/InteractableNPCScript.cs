using UnityEngine;
using TMPro;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

[System.Serializable]
public class Dialog
{
    public string name;
    public string dialog;
    public Sprite expression;
}

public class InteractableNPCScript : MonoBehaviour
{
    public TextMeshProUGUI interactKey;
    // public string[] dialog;
    bool isInDialog = false;
    int dialogCounter = 0;
    bool isInInteractArea = false;
    float wordDelay = 0.09f;

    // dialog ui
    [Header("Dialog UI")]
    public TextMeshProUGUI dialogtext;
    public TextMeshProUGUI dialogName;
    public UnityEngine.UI.Image dialogBackground;
    public UnityEngine.UI.Image characterExpression;

    [Header("Audio")]
    public AudioClip clip;
    public AudioSource audioSource;
    PlayerControllerScript playerController;

    [Header("Dialog")]
    public Dialog[] dialogList;
    // public Dictionary<string, Sprite> dialogDict = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInDialog)
        {
            hideDialog();
        }
        if (Input.GetKeyDown(KeyCode.E) && isInInteractArea == true && isInDialog == false)
        {
            playerController.LoadPlayer();
            if (isInInteractArea) interactKey.enabled = false;
            showDialog();

        }
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
            if (isInInteractArea) interactKey.enabled = true;
            hideDialog();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerControllerScript>();
            interactKey.enabled = true;
            isInInteractArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = null;
            interactKey.enabled = false;
            isInInteractArea = false;
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
