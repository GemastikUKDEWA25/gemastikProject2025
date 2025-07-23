using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnterNameCanvasController : MonoBehaviour
{
    public GameObject EnterNameCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnterNameCanvas.SetActive(false);
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
        EnterNameCanvas.SetActive(true);
    }

    public void hideEnterNameCanvas() {
        EnterNameCanvas.SetActive(false);
    }
    
    
}
