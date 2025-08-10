using UnityEngine;

public class villageIntroduceCutsceneControl2 : MonoBehaviour
{
    public Transform Ddwarf;
    public Transform kayana;
    public Transform player;
    public UnityEngine.Playables.PlayableDirector director;
    SceneController sceneController;

    public CutsceneControlScript cutsceneControlScript;

    private double pauseTime;
    private bool isPaused = false;
    private float maxDistance = 2f;
    public bool isAllowed = true;

    void Start()
    {
        director = GetComponent<UnityEngine.Playables.PlayableDirector>();
        sceneController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SceneController>();
    }

    void Update()
    {
        if (sceneController.guideText.text == "Find and catch the trashes and throw it in the rubbish bin")
        {
            cutsceneControlScript.ResumeDirector();
        }
        if (kayana != null && player != null && isAllowed)
        {
            float distancePlayer = Vector2.Distance(kayana.position, player.position);

            Debug.Log(distancePlayer);
            if (distancePlayer > maxDistance && !isPaused)
            {
                if (sceneController.guideText.text != "Find and catch the trashes and throw it in the rubbish bin")
                {
                    sceneController.guideText.enabled = true;
                    sceneController.guideText.text = "Too Far From Kayana";
                    timeStamp();

                }
            }
            if (distancePlayer < maxDistance && isPaused)
            {
                if (sceneController.guideText.text == "Too Far From Kayana")
                {
                    sceneController.guideText.enabled = false;
                    sceneController.guideText.text = "";
                }
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
