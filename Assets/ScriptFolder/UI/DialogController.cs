using UnityEngine;
using TMPro;
public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI dialogtext;
    public TextMeshProUGUI dialogName;
    public UnityEngine.UI.Image dialogBackground;
    public UnityEngine.UI.Image characterExpression;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // hideDialog();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showDialog()
    {
        gameObject.SetActive(true);
    }

    public void hideDialog()
    {
        gameObject.SetActive(false);
    }

    public void resetDialog()
    {
        dialogName.text = "";
        dialogtext.text = "";
    }
    
}
