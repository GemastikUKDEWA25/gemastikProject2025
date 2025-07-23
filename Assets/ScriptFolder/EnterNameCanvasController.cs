using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnterNameCanvasController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hideEnterNameCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hideEnterNameCanvas();
        }
    }

    public void showEnterNameCanvas()
    {
        gameObject.SetActive(true);
    }

    public void hideEnterNameCanvas() {
        gameObject.SetActive(false);
    }
    
    
}
