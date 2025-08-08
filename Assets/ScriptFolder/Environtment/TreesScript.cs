using UnityEngine;

public class TreesScript : MonoBehaviour
{
    public bool isGoRight = false;
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
        transform.position += move * 1f * Time.deltaTime;
    }
    

}
