using UnityEngine;

public class EnemyVisionScript : MonoBehaviour
{
    public static EnemyVisionScript instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void goLeft()
    {
        Quaternion rotate = transform.rotation;
        rotate.z = -90;
    }
    public void goRight()
    {
        Quaternion rotate = transform.rotation;
        rotate.z = 90;
    }
    public void goUp()
    {
        Quaternion rotate = transform.rotation;
        rotate.z = 180;
    }
    public void goDown()
    {
        Quaternion rotate = transform.rotation;
        rotate.z = 0;
    }
    
}
