using UnityEngine;

public class villageIntroduceCutsceneControl3 : MonoBehaviour
{
    public InteractableNPCScript Chief;
    public InteractableNPCScript kayanaDialog;
    public Transform kayana;
    public Transform player;
    public UnityEngine.Playables.PlayableDirector director;
    SceneController sceneController;

    private double pauseTime;
    private bool isPaused = false;
    private float maxDistance = 2f;

    bool isIndialog = true;

    void Start()
    {
        director = GetComponent<UnityEngine.Playables.PlayableDirector>();
        sceneController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SceneController>();
        timeStamp();
        PauseDirector();
        sceneController.guideText.text = "Interact with Chief";
        Chief.isInDialog = true;
        // Ensure playerController is set before dialog
        // Ddwarf.playerController = GameObject.FindWithTag("Player").GetComponent<PlayerControllerScript>();
        // Ddwarf.showDialog();
    }

    void Update()
    {
        if (sceneController.guideText.text != "Talk to Kayana when you are ready")
        {
            isIndialog = Chief.isInDialog;
            if (kayana != null && player != null)
            {
                float distancePlayer = Vector2.Distance(kayana.position, player.position);
                Debug.Log(distancePlayer);
                if (!isPaused)
                {
                    timeStamp();
                }
                if (isPaused && !isIndialog)
                {
                    sceneController.guideText.text = "";
                    ResumeDirector();
                }

                if (isPaused)
                {
                    PauseDirector();
                }
            }
        }
        else
        {
            Dialog newDialog = new Dialog();
            newDialog.name = "Kayana";
            newDialog.dialog = "Great!, Lets Go";

            Dialog newDialog1 = new Dialog();
            newDialog1.name = "ChangeScene";
            newDialog1.dialog = "SceneVillageIntroduce4";

            Dialog[] newDialogList = {newDialog,newDialog1 };
            kayanaDialog.dialogList = newDialogList;
        }
    }

    public void timeStamp()
    {
        pauseTime = director.time;
        isPaused = true;
    }
    public void PauseDirector()
    {
        if (director != null)
        {
            director.time = pauseTime;
            director.Evaluate();
            isPaused = true;
        }
    }

    public void ResumeDirector()
    {
        isPaused = false;
    }
}
