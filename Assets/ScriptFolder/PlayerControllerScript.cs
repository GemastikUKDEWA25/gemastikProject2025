using System.Data.Common;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private float moveSpeed = 1.5f;
    private float sprintSpeed = 1f;
    public int Health = 100;
    public int stage = 0;

    bool isInDialog;
    public static PlayerControllerScript Instance { get; private set; }

    SpriteRenderer spriteRenderer;
    Animator animator;

    string currentState = "";
    string lastDirection = "down";

    public AudioClip footStepSound; // Assign in Inspector
    private AudioSource audioSource;


    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = footStepSound;
        audioSource.loop = true;
    }

    void Update()
    {
        if (!isInDialog)
        {
            handleMovement();
        }
    }

    public void saving()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        SaveFile data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            // Restore position
            Vector3 pos = new Vector3(data.position[0], data.position[1], data.position[2]);
            transform.position = pos;

            // Restore health
            Health = data.health;
        }
    }

    void handleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        bool isMoving = false;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.up;
            lastDirection = "up";
            ChangeAnimationState("RunBack");
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.down;
            lastDirection = "down";
            ChangeAnimationState("RunFront");
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
            lastDirection = "left";
            ChangeAnimationState("RunLeft");
            // spriteRenderer.flipX = false;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
            lastDirection = "right";
            ChangeAnimationState("RunRight");
            // spriteRenderer.flipX = true;
            isMoving = true;
        }

        if (isMoving)
        {
            if (!audioSource.isPlaying) audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
        }

        // If not moving, play the appropriate idle animation
        if (!isMoving)
        {
            switch (lastDirection)
            {
                case "up":
                    ChangeAnimationState("IdleBack");
                    break;
                case "down":
                    ChangeAnimationState("IdleFront");
                    break;
                case "left":
                    ChangeAnimationState("IdleLeft");
                    break;
                case "right":
                    ChangeAnimationState("IdleRight");
                    break;
            }
        }

        float moveSpeedTemp = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) moveSpeedTemp += sprintSpeed;

        moveDirection = moveDirection.normalized;
        transform.position += moveDirection * moveSpeedTemp * Time.deltaTime;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }

    public void setIsInDialog(bool status)
    {
        isInDialog = status;
    }
    
}
