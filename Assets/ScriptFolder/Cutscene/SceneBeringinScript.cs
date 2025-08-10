using UnityEngine;
using TMPro;
using System.Collections;

[System.Serializable]
public class DialogCutscene
{
    public string name;
    public string dialog;
    public AudioClip voiceSound;
    public Sprite expression;
}

public class SceneBeringinScript : MonoBehaviour
{
    public GameObject MovingButtonInstruction;
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


    float timer = 0;
    bool isInDialog = false;
    int dialogCounter = 0;
    float wordDelay = 0.09f;
    double pauseTime;
    bool isInEnterNameState = false;
    bool isInstructionShowed = false;

    void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        nameTF.SetActive(false);
        playerController.setIsInDialog(true);
        MovingButtonInstruction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInDialog)
        {
            director.time = pauseTime;
            director.Evaluate(); // Apply the state at this time
        }
        if(isInstructionShowed){
            timer += Time.deltaTime;
            if (timer >= 5f)
            {
                MovingButtonInstruction.GetComponent<Animator>().Play("AWSDanimationFadeOut");
                isInstructionShowed = false;
            }
        }


        if (isInEnterNameState && nameInputField.text != "" && Input.GetKey(KeyCode.Return))
        {
            isInEnterNameState = false;

            SaveSystem.SavePlayerName(nameInputField.text.Trim());
            playerController.LoadName();
            Debug.Log(playerController.playerName);

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
            dialog.hideDialog();
        }
    }
    

    public void ShowDialogEvent()
    {
        if (!isInDialog)
        {
            if (director != null)
            {
                pauseTime = director.time;
                director.time = pauseTime;
                director.Evaluate();
            }
            dialogCounter = 0;
            showDialog();
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
            string nameDialog = dialogList[dialogCounter].name;
            string[] dialogSplit = dialogList[dialogCounter].dialog.Split(" ");

            if (nameDialog == "Player") {dialog.dialogName.text = playerController.playerName;}
            else dialog.dialogName.text = nameDialog;

            if (dialogList[dialogCounter].expression != null) dialog.characterExpression.sprite = dialogList[dialogCounter].expression;
            
            StartCoroutine(TypeText(dialogSplit,dialogList[dialogCounter].voiceSound));
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

    public void showInstruction()
    {
        MovingButtonInstruction.SetActive(true);
        if (playerController != null) playerController.setIsInDialog(false);
        isInstructionShowed = true;
    }

    IEnumerator TypeText(string[] dialogSplit,AudioClip voiceSound)
    {       
        foreach (string word in dialogSplit)
        {
            if (isInDialog)
            {
                if (word == "Player")
                {
                    dialog.dialogtext.text += playerController.playerName + " ";
                }
                else
                {
                    dialog.dialogtext.text += word + " ";
                }
                audioSource.PlayOneShot(voiceSound);
                yield return new WaitForSeconds(wordDelay);
            }
        }
    }
}
