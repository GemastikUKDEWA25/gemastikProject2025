using UnityEngine;
using TMPro;
using System.Collections;
using System;
using UnityEngine.UI;

[System.Serializable]
public class DialogCutscene
{
    public string name;
    public string dialog;
    public Sprite expression;
}

public class DialogCutsceneScript : MonoBehaviour
{
    public UnityEngine.Playables.PlayableDirector director;
    public DialogController dialog;
    public GameObject nameTF;
    public TMP_InputField nameInputField;

    [Header("Audio")]
    public AudioClip clip;
    public AudioClip bellSound;
    public AudioSource audioSource;
    
    public PlayerControllerScript playerController;
    public DialogCutscene[] dialogList;


    bool isInDialog = false;
    int dialogCounter = 0;
    float wordDelay = 0.09f;
    double pauseTime;
    bool isInEnterNameState = false;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        playerController.setIsInDialog(true);
        nameTF.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInDialog)
        {
            director.time = pauseTime;
            director.Evaluate(); // Apply the state at this time

        }

        if (isInEnterNameState && nameInputField.text != "" && Input.GetKey(KeyCode.Return))
        {
            isInEnterNameState = false;
            nameTF.SetActive(false);
            nextDialog();
        }
        if (isInDialog && !isInEnterNameState && Input.GetKeyDown(KeyCode.Space))
        {
            nextDialog();
        }
        if (dialogCounter >= dialogList.Length)
        {
            isInDialog = false;
            dialogCounter = 0;
            if (playerController != null) playerController.setIsInDialog(false);
            dialog.hideDialog();
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
        }
    }
    void showDialog()
    {
        if (playerController != null) playerController.setIsInDialog(true);
        isInDialog = true;

        if (dialogList[dialogCounter].name == "EnterName")
        {
            audioSource.PlayOneShot(bellSound);
            showNameTextField();
        }
        else
        {
            dialog.showDialog();
            string[] dialogSplit = dialogList[dialogCounter].dialog.Split(" ");
            dialog.dialogName.text = dialogList[dialogCounter].name;
            if (dialogList[dialogCounter].expression != null) dialog.characterExpression.sprite = dialogList[dialogCounter].expression;
            StartCoroutine(TypeText(dialogSplit));
        }
    }


    void nextDialog()
    {
        dialogCounter += 1;
        dialog.resetDialog();
        if (dialogCounter < dialogList.Length) showDialog();
    }

    public void showNameTextField()
    {
        dialog.hideDialog();
        isInEnterNameState = true;
        nameTF.SetActive(true);
    }

    IEnumerator TypeText(string[] dialogSplit)
    {       
        foreach (string word in dialogSplit)
        {
            if (isInDialog)
            {                
                dialog.dialogtext.text += word + " ";
                audioSource.PlayOneShot(clip);
                yield return new WaitForSeconds(wordDelay);
            }
        }
    }
}
