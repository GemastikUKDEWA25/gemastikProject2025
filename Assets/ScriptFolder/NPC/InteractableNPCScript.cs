using UnityEngine;
using TMPro;
using System.Collections;
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
    public static InteractableNPCScript activeNPC; // The NPC currently in dialog

    public SpriteRenderer spriteRenderer;
    bool isInDialog = false;
    int dialogCounter = 0;
    bool isInInteractArea = false;
    float wordDelay = 0.09f;
    SceneController sceneController;

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
        sceneController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SceneController>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Start dialog
        if (Input.GetKeyDown(KeyCode.E) && isInInteractArea && activeNPC == null)
        {
            showDialog();
            if (isInInteractArea) spriteRenderer.enabled = false;
        }

        // Progress dialog
        if (Input.GetKeyDown(KeyCode.Space) && activeNPC == this)
        {
            dialogCounter += 1;
            if (dialogCounter < dialogList.Length)
            {
                dialog.resetDialog();
                showDialog();
            }
            else
            {
                hideDialog(); // End dialog
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerControllerScript>();
            if (activeNPC == null) spriteRenderer.enabled = true;
            isInInteractArea = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerControllerScript>();
            if (activeNPC == null) spriteRenderer.enabled = true;
            isInInteractArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = null;
            spriteRenderer.enabled = false;
            isInInteractArea = false;

            // If this NPC was the one talking, end dialog
            if (activeNPC == this)
            {
                hideDialog();
            }
        }
    }

    void showDialog()
    {
        activeNPC = this; // Mark this NPC as the one talking
        isInDialog = true;
        if (playerController != null) playerController.setIsInDialog(true);

        string[] dialogSplit = dialogList[dialogCounter].dialog.Split(" ");
        string nameDialog = dialogList[dialogCounter].name;
        dialog.showDialog();

        if (nameDialog.Trim() == "ChangeScene")
        {
            sceneController.changeScene(dialogList[dialogCounter].dialog);
            hideDialog();
            return;
        }
        if (nameDialog.Trim() == "Player")
        {
            playerController.LoadName();
            dialog.dialogName.text = playerController.playerName;
        }
        else dialog.dialogName.text = nameDialog;

        if (dialogList[dialogCounter].expression != null)
            dialog.characterExpression.sprite = dialogList[dialogCounter].expression;

        StartCoroutine(TypeText(dialogSplit, dialogList[dialogCounter].sound));
    }

    void hideDialog()
    {
        if (activeNPC != this) return; // Only the active NPC can hide the dialog

        isInDialog = false;
        if (playerController != null) playerController.setIsInDialog(false);
        dialogCounter = 0;
        dialog.resetDialog();
        dialog.hideDialog();

        activeNPC = null; // No NPC is talking anymore
        if (isInInteractArea) spriteRenderer.enabled = true;
    }

    IEnumerator TypeText(string[] dialogSplit, AudioClip voiceSound)
    {
        foreach (string word in dialogSplit)
        {
            if (isInDialog && activeNPC == this)
            {
                if (word == "Player")
                {
                    playerController.LoadName();
                    dialog.dialogtext.text += playerController.playerName + " ";
                }
                else
                {
                    dialog.dialogtext.text += word + " ";
                }

                if (voiceSound != null)
                    audioSource.PlayOneShot(voiceSound);

                yield return new WaitForSeconds(wordDelay);
            }
        }
    }
}
