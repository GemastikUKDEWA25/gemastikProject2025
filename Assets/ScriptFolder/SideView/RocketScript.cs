using UnityEngine;

public class RocketScript : MonoBehaviour
{   
    float moveSpeed = 30f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.A)) moveDirection += Vector3.left;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
