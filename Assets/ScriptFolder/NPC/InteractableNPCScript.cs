using UnityEngine;
using System.Collections;

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
    public static InteractableNPCScript activeNPC; // NPC currently in dialog

    public SpriteRenderer spriteRenderer;
    public bool isInDialog = false;
    int dialogCounter = 0;
    bool isInInteractArea = false;
    float wordDelay = 0.09f;
    SceneController sceneController;

    // Dialog UI
    [Header("Dialog UI")]
    DialogController dialog;

    [Header("Audio")]
    public AudioClip clip;
    public AudioSource audioSource;
    PlayerControllerScript playerController;
    public CutsceneControlScript cutsceneControlScript;

    [Header("Dialog")]
    public Dialog[] dialogList;

    // Typing control
    Coroutine typingCoroutine;
    bool isTyping = false;

    void Start()
    {
        dialog = GameObject.FindGameObjectWithTag("DialogCanvas").GetComponent<DialogController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
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

        // Progress or skip dialog
        if (Input.GetKeyDown(KeyCode.Space) && activeNPC == this)
        {
            if (isTyping)
            {
                // Skip typing → instantly show full line
                StopCoroutine(typingCoroutine);
                finishCurrentLine();
            }
            else
            {
                // Go to next line
                dialogCounter++;
                if (dialogCounter < dialogList.Length)
                {
                    dialog.resetDialog();
                    showDialog();
                }
                else
                {
                    dialog.resetDialog();
                    hideDialog();
                }
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

            if (activeNPC == this) // if talking, end dialog
            {
                hideDialog();
            }
        }
    }

    public void showDialog()
    {
        if (playerController == null) playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
        if (dialog == null) dialog = GameObject.FindGameObjectWithTag("DialogCanvas").GetComponent<DialogController>();

        activeNPC = this;
        isInDialog = true;
        if (playerController != null) playerController.setIsInDialog(true);
        dialog.resetDialog();
        string[] dialogSplit = dialogList[dialogCounter].dialog.Split(" ");
        string nameDialog = dialogList[dialogCounter].name;
        dialog.showDialog();

        // Special command: ChangeScene
        if (nameDialog.Trim() == "ChangeScene")
        {
            sceneController.changeScene(dialogList[dialogCounter].dialog);
            hideDialog();
            return;
        }
        if (nameDialog.Trim() == "GuideText")
        {
            sceneController.guideText.text = dialogList[dialogCounter].dialog;
            hideDialog();

            if (cutsceneControlScript != null)
            {
                cutsceneControlScript.ResumeDirector();
            }
            return;
        }

        // Special name replacement
        if (nameDialog.Trim() == "Player")
        {
            playerController.LoadName();
            dialog.dialogName.text = playerController.playerName;
        }
        else
        {
            dialog.dialogName.text = nameDialog;
        }

        // Expression
        if (dialogList[dialogCounter].expression != null)
            dialog.characterExpression.sprite = dialogList[dialogCounter].expression;

        // Stop previous coroutine if still typing
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(dialogSplit, dialogList[dialogCounter].sound));
    }

    void hideDialog()
    {
        if (activeNPC != this) return; // Only active NPC can hide

        isInDialog = false;
        if (playerController != null) playerController.setIsInDialog(false);
        dialogCounter = 0;
        dialog.resetDialog();
        dialog.hideDialog();

        activeNPC = null;
        if (isInInteractArea) spriteRenderer.enabled = true;
    }

    IEnumerator TypeText(string[] dialogSplit, AudioClip voiceSound)
    {
        isTyping = true;

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

        isTyping = false; // finished
    }

    void finishCurrentLine()
    {
        isTyping = false;
        string finalText = dialogList[dialogCounter].dialog.Replace("Player", playerController.playerName);
        dialog.dialogtext.text = finalText;
    }
}
