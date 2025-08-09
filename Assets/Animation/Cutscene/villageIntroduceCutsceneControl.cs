using UnityEngine;

public class VillageIntroduceCutsceneControl : MonoBehaviour
{
    public Transform Ddwarf;
    public Transform kayana;
    public Transform player;
    public UnityEngine.Playables.PlayableDirector director;

    private double pauseTime;
    private bool isPaused = false;
    private float maxDistance = 2f;

    void Start()
    {
        director = GetComponent<UnityEngine.Playables.PlayableDirector>();
    }

    void Update()
    {
        float distancePlayer = Vector2.Distance(kayana.position, player.position);
        float distanceDdwarf = Vector2.Distance(kayana.position, Ddwarf.position);

        Debug.Log(distancePlayer);
        // Example: Pause if player is too far from Kayana
        if (distancePlayer > maxDistance && !isPaused)
        {
            timeStamp();
        }
        if (distancePlayer < maxDistance && isPaused)
        {
            ResumeDirector();
        }

        if (isPaused)
        {
            PauseDirector();
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
