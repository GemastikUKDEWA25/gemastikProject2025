using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public SpriteRenderer icon;

    public string Carry = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public void carryObject(Sprite carriedObject, string carriedObjectName)
    {
        icon.enabled = true;
        icon.sprite = carriedObject;
        Carry = carriedObjectName;
    }
    
    public void putDownCarriedObject()
    {
        icon.enabled = false;
        icon.sprite = null;
        Carry = "";
    }
}
