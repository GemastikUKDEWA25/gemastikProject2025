using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnterNameCanvasController : MonoBehaviour
{
    public Image EnterNameCanvasBg;
    public Image textFieldNameBackground;
    public TextMeshProUGUI textFieldNameBackgroundPlaceHolder;
    public TextMeshProUGUI label;

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
        EnterNameCanvasBg.enabled = true;
        textFieldNameBackground.enabled = true;
        textFieldNameBackgroundPlaceHolder.enabled = true;
        label.enabled = true;
    }

    public void hideEnterNameCanvas() {
        EnterNameCanvasBg.enabled = false;
        textFieldNameBackground.enabled = false;
        textFieldNameBackgroundPlaceHolder.enabled = false;
        label.enabled = false;
    }
    
    
}
