using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController instance { get; private set; }
    [SerializeField] Animator transitionSceneAnimation; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
}
