using System;
using UnityEngine;

public class TrashMonsterScript : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform trash;
    Transform target;
    Animator animator;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float stopDistance = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the Player has the 'Player' tag.");
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            // Move toward player
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            // Update animation speed
            animator.SetFloat("Velocity", rb.linearVelocity.magnitude);


            if (target.position.x < transform.position.x)
            {
                if (transform.transform.localScale.x > 0)
                {
                    trash.transform.localScale = new Vector3(trash.transform.localScale.x * -1, trash.transform.localScale.y, trash.transform.localScale.y);
                }
            }
            if (target.position.x > transform.position.x)
            {
                if (transform.transform.localScale.x < 0)
                {
                    trash.transform.localScale = new Vector3(trash.transform.localScale.x*-1, trash.transform.localScale.y, trash.transform.localScale.y);
                }
            }
            
        }
        else
        {
            // Stop movement when close
            rb.linearVelocity = Vector2.zero;
            animator.SetFloat("Velocity", 0f);
        }
    }
}
