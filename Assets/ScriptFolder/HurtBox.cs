using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public GolemScript golemHurtBox;
    public SpriteRenderer[] spriteRenderer;
    public bool isCore = false;

    [Header("Hit Flash")]
    public float flashDuration = 0.2f;

    bool flashing;
    float flashUntil;

    public void changeColor() // keep your API; now it triggers a timed flash
    {
        flashing = true;
        flashUntil = Time.time + flashDuration;
    }

    public void attack(float damage)
    {
        if (!isCore) return;
        changeColor();
        golemHurtBox.attack(damage);
    }

    void LateUpdate()
    {
        if (!flashing) return;

        // Force red AFTER Animator runs
        for (int i = 0; i < spriteRenderer.Length; i++)
            if (spriteRenderer[i]) spriteRenderer[i].color = Color.red;

        if (Time.time >= flashUntil)
        {
            flashing = false;
            // restore
            for (int i = 0; i < spriteRenderer.Length; i++)
                if (spriteRenderer[i]) spriteRenderer[i].color = Color.white;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            var magicDagger = collision.gameObject.GetComponent<MagicDaggerScript>();
            attack(magicDagger.damage);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            var magicDagger = collision.GetComponent<MagicDaggerScript>();
            attack(magicDagger.damage);
        }
    }
}
