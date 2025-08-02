using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class HidingScript : MonoBehaviour
{
    public CinemachineCamera cinemachineCamera;

    private GameObject player;
    private SpriteRenderer playerRenderer;
    private PlayerInput playerInput; // or your movement script
    LineTesting enemy;
    private bool isHiding = false;
    private bool isPlayerInside = false;

    bool triggerSetHidingToEnemy = false;

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("PatrolEnemy").GetComponent<LineTesting>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRenderer = player.GetComponent<SpriteRenderer>();
        playerInput = player.GetComponent<PlayerInput>(); // replace with your actual movement script if needed
    }

    private void Update()
    {
        if (isHiding && !triggerSetHidingToEnemy)
        {
            enemy.setIsPlayerHiding(true);
            triggerSetHidingToEnemy = true;
        }
        if (!isHiding && triggerSetHidingToEnemy)
        {
            enemy.setIsPlayerHiding(false);
            triggerSetHidingToEnemy = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && isPlayerInside)
        {
            if (!isHiding)
            {
                // Hide player visually and disable input
                playerRenderer.enabled = false;
                player.layer = LayerMask.NameToLayer("Default"); // Change "Enemy" to your target layer name

                if (playerInput != null) playerInput.enabled = false;

                // Switch camera to hiding spot
                cinemachineCamera.Follow = transform;
                cinemachineCamera.LookAt = transform;

                isHiding = true;
            }
            else
            {
                // Unhide player and re-enable input
                playerRenderer.enabled = true;
                player.layer = LayerMask.NameToLayer("Player"); // Change "Enemy" to your target layer name

                if (playerInput != null) playerInput.enabled = true;

                // Switch camera back to player
                cinemachineCamera.Follow = player.transform;
                cinemachineCamera.LookAt = player.transform;

                isHiding = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;

            // Auto unhide if player exits while hiding
            if (isHiding)
            {
                playerRenderer.enabled = true;
                if (playerInput != null) playerInput.enabled = true;

                cinemachineCamera.Follow = player.transform;
                cinemachineCamera.LookAt = player.transform;

                isHiding = false;
            }
        }
    }
}
