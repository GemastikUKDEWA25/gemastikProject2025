using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController instance { get; private set; }
    public GameObject menu;
    public DialogController dialogController;
    public TextMeshProUGUI guideText;
    GameObject player;
    bool isPaused;
    [SerializeField] Animator transitionSceneAnimation;
    private void Awake()
    {
        // if (instance == null)
        // {
        //     instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else if (instance != this)
        // {
        //     Destroy(gameObject); // Destroy duplicate
        // }

    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        menu.SetActive(false);
        dialogController.hideDialog();
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "MainMenu") SaveSystem.SavePlayerStage(sceneName);
        if (player != null) LoadPosition(player.transform);
        // guideText.enabled = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();

        }
    }

    public void changeScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        transitionSceneAnimation.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        transitionSceneAnimation.SetTrigger("Start");
        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainMenu") return;
        Time.timeScale = 0f;
        isPaused = true;
        menu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        menu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        changeScene("MainMenu");
        ResumeGame();
        menu.SetActive(false);
        // SceneManager.LoadScene("MainMenu");
    }

    public void loadCheckPoint()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        changeScene(sceneName);
    }

    public void LoadPosition(Transform playerTransform)
    {
        SaveFile data = SaveSystem.LoadPlayer();
        string sceneName = SceneManager.GetActiveScene().name;
        if (data != null)
        {
            // changeScene(data.stage); // this makes me change scene back and forth;
            Debug.Log("Stage: ");
            Debug.Log(data.stage);
            for (int i = 0; i < data.stageKeys.Count; i++)
            {
                Debug.Log(data.stageKeys[i]);
                Debug.Log(data.stagePositions[i]);
                if (data.stageKeys[i] == sceneName)
                {
                    Vector3 pos = new Vector3(data.stagePositions[i][0], data.stagePositions[i][1], data.stagePositions[i][2]);
                    playerTransform.position = pos;
                    break;
                }
            }
        }
    }
}
