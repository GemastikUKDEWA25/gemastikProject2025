using UnityEngine;

public class TreesScript : MonoBehaviour
{
    public bool isGoRight = false;
    public float movementSpeed = 1f;
    void Start()
    {

    }

    void Update()
    {
        Vector3 move = Vector3.zero;
        if (!isGoRight)
        {
            move += Vector3.left;
        }
        else
        {
            move += Vector3.right;
        }
        move = move.normalized;
        transform.position += move * movementSpeed * Time.deltaTime;
    }
    

}
