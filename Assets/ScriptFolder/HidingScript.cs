using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HidingScript : MonoBehaviour
{
    public CinemachineCamera cinemachineCamera;

    GameObject player;
    Vector3 lastPosition;
    bool hiding = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hiding)
            {
                // Vector3 pos = lastPosition;
                // pos.y += 1000f;
                // player.transform.position = pos;
                // player.layer = LayerMask.NameToLayer("Default");
                player.SetActive(false);

                cinemachineCamera.Follow = transform;
                cinemachineCamera.LookAt = transform;

                hiding = true;
            }
            else
            {
                // player.layer = LayerMask.NameToLayer("Player");
                player.SetActive(true);
                cinemachineCamera.Follow = player.transform;
                cinemachineCamera.LookAt = player.transform;
                hiding = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hide");
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            lastPosition = player.transform.position;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Hide");
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            lastPosition = player.transform.position;
        }
    }
}
