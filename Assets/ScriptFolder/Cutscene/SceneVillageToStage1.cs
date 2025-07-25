using UnityEngine;
using TMPro;
using System.Collections;

[System.Serializable]
public class DialogVillageToStage1
{
    public string name;
    public string dialog;
    public Sprite expression;
}


public class SceneVillageToStage1 : MonoBehaviour
{
    public DialogController dialog;
    [Header("Audio")]
    public AudioClip clip;
    public AudioSource audioSource;
    
    public DialogCutscene[] dialogList;
    int dialogCounter = 0;
    float wordDelay = 0.09f;
    SaveFile data;
    bool isInDialog = false;
    float timer = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        data = SaveSystem.LoadPlayer();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (!isInDialog && timer < 0)
        {
            showDialog();
            isInDialog = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextDialog();
        }
        if (dialogCounter >= dialogList.Length)
        {
            dialogCounter = 0;
            dialog.hideDialog();
        }
    }

    void showDialog()
    {

        dialog.showDialog();
        string nameDialog = dialogList[dialogCounter].name;
        string[] dialogSplit = dialogList[dialogCounter].dialog.Split(" ");
        if (nameDialog == "Player") {dialog.dialogName.text = data.name;}
        else dialog.dialogName.text = nameDialog;
        if (dialogList[dialogCounter].expression != null) dialog.characterExpression.sprite = dialogList[dialogCounter].expression;
        StartCoroutine(TypeText(dialogSplit));
        
    }


    void nextDialog()
    {
        dialogCounter += 1;
        dialog.resetDialog();
        if (dialogCounter < dialogList.Length) showDialog();
    }


    IEnumerator TypeText(string[] dialogSplit)
    {       
        foreach (string word in dialogSplit)
        {               
            dialog.dialogtext.text += word + " ";
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(wordDelay);

        }
    }
}
