using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController instance { get; private set; }
    public GameObject menu;
    bool isPaused;
    [SerializeField] Animator transitionSceneAnimation;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            // Destroy the old instance (the one already stored)
            Destroy(instance.gameObject);
        }

        // Assign this as the current instance
        instance = this;

        // Make this object persist between scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        menu.SetActive(false);
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
}
