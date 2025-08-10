using UnityEngine;
using TMPro;
public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI dialogtext;
    public TextMeshProUGUI dialogName;
    public UnityEngine.UI.Image dialogBackground;
    public UnityEngine.UI.Image characterExpression;
    public TextMeshProUGUI nextButtonText;
    public UnityEngine.UI.Image nextButtonBackground;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hideDialog();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showDialog()
    {
        // gameObject.SetActive(true);
        dialogtext.enabled = true;
        dialogName.enabled = true;
        dialogBackground.enabled = true;
        characterExpression.enabled = true;

        nextButtonText.enabled = true;
        nextButtonBackground.enabled = true;

    }

    public void hideDialog()
    {
        dialogtext.enabled = false;
        dialogName.enabled = false;
        dialogBackground.enabled = false;
        characterExpression.enabled = false;

        nextButtonText.enabled = false;
        nextButtonBackground.enabled = false;
    }

    

    public void resetDialog()
    {
        dialogName.text = "";
        dialogtext.text = "";
    }
    
}
