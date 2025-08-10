    using UnityEngine;
    using TMPro;
    using System.Collections;
    using UnityEditor.SearchService;

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
        public SceneController sceneController;
        [Header("Audio")]
        public AudioClip clip;
        public AudioSource audioSource;
        public string NextSceneName;
        public DialogCutscene[] dialogList;
        
        int dialogCounter = 0;
        float wordDelay = 0.09f;
        SaveFile data;
        bool isInDialog = false;
        bool isFinished = false;
        public float timer = 3f;
        bool hasChangedScene = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (audioSource == null) audioSource = GetComponent<AudioSource>();
            data = SaveSystem.LoadPlayer();
        }

    void Update()
    {
        if (!isFinished)
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

            if (isInDialog)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    nextDialog();
                }
                if (dialogCounter >= dialogList.Length)
                {
                    dialog.hideDialog();
                    isInDialog = false;
                    isFinished = true;
                }
            }
        }
        else if (!hasChangedScene)
        {
            hasChangedScene = true;
            sceneController.changeScene(NextSceneName);
        }
    }

    void showDialog()
    {

        dialog.showDialog();
        string nameDialog = dialogList[dialogCounter].name;
        string[] dialogSplit = dialogList[dialogCounter].dialog.Split(" ");
        if (nameDialog == "Player")
        {
            if (data.name != null)
            {
                dialog.dialogName.text = data.name;
            }
            else dialog.dialogName.text = "Player";
        }
        else dialog.dialogName.text = nameDialog;
        if (dialogList[dialogCounter].expression != null) dialog.characterExpression.sprite = dialogList[dialogCounter].expression;
        StartCoroutine(TypeText(dialogSplit,dialogList[dialogCounter].voiceSound));
            
        }


        void nextDialog()
        {
            dialogCounter += 1;
            dialog.resetDialog();
            if (dialogCounter < dialogList.Length) showDialog();
        }


        IEnumerator TypeText(string[] dialogSplit, AudioClip audio)
        {       
            foreach (string word in dialogSplit)
            {               
                dialog.dialogtext.text += word + " ";
                audioSource.PlayOneShot(audio);
                yield return new WaitForSeconds(wordDelay);

            }
        }
    }
