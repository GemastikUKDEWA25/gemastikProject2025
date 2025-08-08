using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour
{
    public GameObject Credits;
    public Animator creditsAnimator;
    public SceneController sceneController;
    AnimatorStateInfo stateInfo;

    private bool isInCredits;
    void Start()
    {
        Credits.SetActive(false);
    }

    void Update()
    {
        if (isInCredits)
        {
            stateInfo = creditsAnimator.GetCurrentAnimatorStateInfo(0);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Credits.SetActive(false);
                isInCredits = false;
            }
            if (stateInfo.IsName("CreditsAnimation") && stateInfo.normalizedTime >= 1f)
            {
                Credits.SetActive(false);
                isInCredits = false;
            }
        }

    }

    public void playGame()
    {
        gameObject.SetActive(false);
        // sceneController.changeScene("SceneBeringin");
        SaveFile saveFile = SaveSystem.LoadPlayer();
        if (saveFile.stage != null)
        {
            sceneController.changeScene(saveFile.stage);
        }
        else
        {
            sceneController.changeScene("CutsceneComicScene");
        }
    }
    public void playCredit()
    {
        if (!isInCredits)
        {
            Credits.SetActive(true);
            isInCredits = true;
            creditsAnimator.Play("CreditsAnimation");
        }
    }

    public void deleteSave()
    {
        SaveSystem.DeleteSaveFile();   
    }
}
