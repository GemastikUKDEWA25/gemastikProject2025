using UnityEngine;
using UnityEngine.Playables;
using Unity.Cinemachine;
public class CutsceneControlScript : MonoBehaviour
{
    double pauseTime;
    bool isPaused;
    bool isPausedSignal;
    PlayableDirector director;
    SceneController sceneController;

    public CinemachineCamera cinemachineCamera;


    void Update()
    {
        sceneController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SceneController>();

        if (isPaused && isPausedSignal)
        {
            PauseDirector();
        }
    }


    public void changeScene(string changeScene)
    {
        sceneController.changeScene(changeScene);
    }
    public void timeStamp()
    {
        pauseTime = director.time;
    }
    public void pausedSignal()
    {
        isPausedSignal = true;
    }
    public void PauseDirector()
    {
        if (director != null && isPausedSignal)
        {
            director.time = pauseTime;
            director.Evaluate();
            isPaused = true;
        }
    }
    public void ResumeDirector()
    {
        isPausedSignal = false;
        isPaused = false;
    }
    public void assignPlayableDirector(PlayableDirector director)
    {
        this.director = director;
    }
    public void activateGameobject(GameObject objectGame)
    {
        objectGame.SetActive(true);
    }
    public void deactivateGameobject(GameObject objectGame)
    {
        objectGame.SetActive(false);
    }

    public void talkToNpc(InteractableNPCScript npc)
    {
        npc.isInDialog = true;
        npc.showDialog();
    }

    public void MoveFocusCamera(Transform character)
    {
        cinemachineCamera.Follow = character;
        cinemachineCamera.LookAt = character;
    }




}
