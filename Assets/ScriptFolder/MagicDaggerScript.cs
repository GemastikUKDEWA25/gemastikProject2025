using UnityEngine;
using UnityEngine.Assertions.Must;

public class MagicDaggerScript : MonoBehaviour
{
    Animator animator;
    bool flying = false;
    public string direction = "";
    float timer = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        if (flying && direction == "Right")
        {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;

            move += Vector3.right;
        }
        if (flying && direction == "Left")
        {
            Vector3 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
            
            move += Vector3.left;
        }
        move = move.normalized;
        transform.position += move * 15f * Time.deltaTime;

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            DestroyObject();
        }
    }

    public void setFlying()
    {
        flying = true;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Enemy"))
        {
            if (collision.CompareTag("Enemy"))
            {
                HurtBox enemy = collision.GetComponent<HurtBox>();
                if (enemy != null) {if (enemy.isCore) enemy.attack(10);}
            }
            flying = false;
            animator.SetTrigger("hit");
        }
    }
}
