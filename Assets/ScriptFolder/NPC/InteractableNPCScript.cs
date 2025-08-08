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
    public AudioClip sound;
    public Sprite expression;
}

public class InteractableNPCScript : MonoBehaviour
{
    public TextMeshProUGUI interactKey;
    bool isInDialog = false;
    int dialogCounter = 0;
    bool isInInteractArea = false;
    float wordDelay = 0.09f;

    // dialog ui
    [Header("Dialog UI")]
    DialogController dialog;

    [Header("Audio")]
    public AudioClip clip;
    public AudioSource audioSource;
    PlayerControllerScript playerController;

    [Header("Dialog")]
    public Dialog[] dialogList;

    void Start()
    {
        dialog = GameObject.FindGameObjectWithTag("DialogCanvas").GetComponent<DialogController>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
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
            showDialog();
            if (isInInteractArea) interactKey.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isInDialog == true)
        {
            dialogCounter += 1;
            if (dialogCounter < dialogList.Length)
            {
                dialog.resetDialog();
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
        // Debug.Log(collision.tag);
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerControllerScript>();
            interactKey.enabled = true;
            isInInteractArea = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // Debug.Log(collision.tag);
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
        isInDialog = true;
        if (playerController != null) playerController.setIsInDialog(true);

        string[] dialogSplit = dialogList[dialogCounter].dialog.Split(" ");
        string nameDialog = dialogList[dialogCounter].name;
        dialog.showDialog();
        if (nameDialog.Trim() == "Player")
        {
            playerController.LoadName();
            dialog.dialogName.text = playerController.playerName;
        }
        else dialog.dialogName.text = nameDialog;

        if (dialogList[dialogCounter].expression != null) dialog.characterExpression.sprite = dialogList[dialogCounter].expression;
        StartCoroutine(TypeText(dialogSplit,dialogList[dialogCounter].sound));
    }

    void hideDialog()
    {
        isInDialog = false; 
        if (playerController != null) playerController.setIsInDialog(false);
        dialogCounter = 0;
        dialog.resetDialog();
        dialog.hideDialog();
    }

    IEnumerator TypeText(string[] dialogSplit,AudioClip voiceSound)
    {       
        foreach (string word in dialogSplit)
        {
            if (isInDialog)
            {
                Debug.Log($"word: {word} {word == "Player"}");
                if (word == "Player")
                {
                    playerController.LoadName();
                    dialog.dialogtext.text += playerController.playerName + " ";
                }
                else { dialog.dialogtext.text += word + " "; }
                audioSource.PlayOneShot(voiceSound);
                yield return new WaitForSeconds(wordDelay);
            }
        }
    }
}
