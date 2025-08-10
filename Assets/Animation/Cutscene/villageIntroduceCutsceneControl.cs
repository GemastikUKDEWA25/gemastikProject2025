using TMPro;
using UnityEngine;

public class VillageIntroduceCutsceneControl : MonoBehaviour
{

    public Transform Ddwarf;
    public Transform kayana;
    public Transform player;
    public UnityEngine.Playables.PlayableDirector director;
    SceneController sceneController;

    private double pauseTime;
    private bool isPaused = false;
    private float maxDistance = 2f;

    void Start()
    {
        director = GetComponent<UnityEngine.Playables.PlayableDirector>();
        sceneController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SceneController>();
    }

    void Update()
    {
        
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
