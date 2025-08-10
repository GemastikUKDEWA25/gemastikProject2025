using UnityEngine;

public class villageIntroduceCutsceneControl1 : MonoBehaviour
{
    public InteractableNPCScript Ddwarf;
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
        sceneController.guideText.text = "Interact with D-Dwarf";
        Ddwarf.isInDialog = true;
        // Ensure playerController is set before dialog
        // Ddwarf.playerController = GameObject.FindWithTag("Player").GetComponent<PlayerControllerScript>();
        // Ddwarf.showDialog();
    }

    void Update()
    {
        isIndialog = Ddwarf.isInDialog;
        if (kayana != null && player != null)
        {
            float distancePlayer = Vector2.Distance(kayana.position, player.position);

            Debug.Log(distancePlayer);
            // Example: Pause if player is too far from Kayana
            if (distancePlayer > maxDistance && !isPaused)
            {
                sceneController.guideText.enabled = true;
                sceneController.guideText.text = "Too Far From Kayana";
                timeStamp();
            }
            if (distancePlayer < maxDistance && isPaused && !isIndialog)
            {
                sceneController.guideText.enabled = false;
                sceneController.guideText.text = "";
                ResumeDirector();
            }

            if (isPaused)
            {
                PauseDirector();
            }
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
