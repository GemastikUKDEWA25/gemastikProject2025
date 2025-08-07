using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerSideViewScript : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float sprintSpeed = 1f;
    public float jumpForce = 5f;
    private int doubleJump = 2;
    private Rigidbody2D rb;
    public Animator animator;
    public MagicAttackSpawner magicAttackSpawner;
    public hitAreaScript hitArea;

    public Slider healthBar;
    public Slider manaBar;
    // SpriteRenderer spriteRenderer;
    public bool isSliding = false;
    
    bool isWalledLeft = false;
    bool isWalledRight = false;

    private float slideTimer = 0f;
    float slideDuration = 0.5f;
    string direction = "Right";

    public float health = 100f;
    public float mana = 100f;

    float blockManaConsumption = 20f;

    bool isChargedUp = false;
    float chargingEnergy = 0f;
    float chargedUpManaConsumption = 2f;

    public float knockBackForce;
    public float knockBackCounter;
    public float knockBackTotalTime;
    public bool knockFromRight;


    // public bool isBlocking;

    public float parryTimer = 0.3f;

    public AudioSource audioSourceCombat;
    public AudioSource audioSourceMovement;
    public AudioClip daggerSwing;
    public AudioClip magicShieldSound;
    public AudioClip blockedSound;
    public AudioClip parrySound;
    public AudioClip magicDaggerSound;
    public AudioClip footStep;
    public AudioClip jumpSound;
    public AudioClip doubleJumpSound;
    public AudioClip slidingSound;

    public AudioClip chargedAttack;
    



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthBar.maxValue = 100;
        manaBar.maxValue = 100;

    }

    void Update()
    {
        if (health <= 0) { animator.Play("Dead"); healthBar.fillRect.gameObject.SetActive(false); return; }
        int currentHealth = Mathf.Clamp(Mathf.FloorToInt(health), 0, 100);
        healthBar.value = currentHealth;
        if (mana <= 0) { manaBar.fillRect.gameObject.SetActive(false); }
        else { manaBar.fillRect.gameObject.SetActive(true); }
        int currentMana = Mathf.Clamp(Mathf.FloorToInt(mana), 0, 100);
        manaBar.value = currentMana;

        float moveInput = 0f;
        bool isMoving = false;

        if (direction == "Right" && transform.localScale.x == -1)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
        if (direction == "Left" && transform.localScale.x == 1)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }

        if (!animator.GetBool("isBlocking"))
        {
            if (Input.GetKeyDown(KeyCode.W) && doubleJump > 1)
            {
                animator.SetTrigger("Jump");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // jump
                doubleJump -= 1;

            }
            if (IsGroundedScript.Instance.getGrounded())
            {
                doubleJump = 2;
            }



            if (Input.GetKeyDown(KeyCode.LeftShift) && !isSliding && !isChargedUp)
            {
                slideTimer = slideDuration;
                isSliding = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && isSliding)
            {
                // audioSourceMovement.Stop();
                isSliding = false;
            }


            if (Input.GetKey(KeyCode.A) && !isWalledLeft)
            {
                isMoving = true;
                direction = "Left";
                moveInput -= 1;
                if (hitAreaScript.Instance.getCircleOffset().x > 0) hitAreaScript.Instance.flipCircleOffset();
            }
            if (Input.GetKey(KeyCode.D) && !isWalledRight)
            {
                isMoving = true;
                direction = "Right";
                moveInput += 1;
                if (hitAreaScript.Instance.getCircleOffset().x < 0) hitAreaScript.Instance.flipCircleOffset();
                // animator.Play("RunRight");
            }

            if (isMoving && IsGroundedScript.Instance.getGrounded() && !isSliding)
            {
                if (!audioSourceMovement.isPlaying) audioSourceMovement.PlayOneShot(footStep);
            }
            if (!isMoving || !IsGroundedScript.Instance.getGrounded() || isSliding)
            {
                if (audioSourceMovement.isPlaying) audioSourceMovement.Stop();
            }
        }

        float moveSpeedTemp = moveSpeed;

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            moveSpeedTemp += sprintSpeed;
            if (slideTimer <= 0)
            {
                isSliding = false;
            }
        }

        animator.SetFloat("Health", health);
        animator.SetFloat("Mana", mana);
        animator.SetBool("isGrounded", IsGroundedScript.Instance.getGrounded());
        animator.SetBool("isFacingRight", direction == "Right");
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isSliding", isSliding);
        animator.SetFloat("xVelocity", rb.linearVelocityX);
        animator.SetFloat("yVelocity", rb.linearVelocityY);

        if (Input.GetKeyDown(KeyCode.L) && !animator.GetBool("isBlocking") && mana - blockManaConsumption > 0)
        {
            parryTimer = 0.3f;
            CancelCharging();
            animator.Play("Block");
            animator.SetBool("isBlocking", true);
            // isBlocking = true;
        }
        if (Input.GetKeyUp(KeyCode.L) && animator.GetBool("isBlocking"))
        {
            parryTimer = 0.3f;
            animator.SetBool("isBlocking", false);
            // isBlocking = false;
        }

        if (Input.GetKeyDown(KeyCode.K) && !isChargedUp)
        {
            isChargedUp = true;
            animator.SetBool("ChargedUp", true);
            animator.Play("ChargedAttack");

            audioSourceCombat.PlayOneShot(chargedAttack);
        }
        if (Input.GetKeyUp(KeyCode.K) && isChargedUp)
        {
            animator.SetBool("ChargedUp", false);
            audioSourceCombat.Stop();
        }

        Debug.Log(mana);

        if (knockBackCounter <= 0)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeedTemp, rb.linearVelocity.y);

        }
        else
        {
            if (knockFromRight)
            {
                rb.linearVelocity = new Vector2(-knockBackForce, knockBackForce);
            }
            if (!knockFromRight)
            {
                rb.linearVelocity = new Vector2(knockBackForce, knockBackForce);
            }

            knockBackCounter -= Time.deltaTime;
        }

        // if (mana < blockManaConsumption)
        // {
        //     isBlocking = false;
        // }
        
    }

    void FixedUpdate()
    {
        if (animator.GetBool("isBlocking"))
        {
            if (mana > 10f) mana -= blockManaConsumption * Time.fixedDeltaTime;
            if (parryTimer > 0) parryTimer -= Time.deltaTime;
        }
        if (isChargedUp)
        {
            if (mana > 10f)
            {
                if (chargingEnergy < 20f)
                {
                    mana -= chargedUpManaConsumption * Time.fixedDeltaTime;
                    chargingEnergy += chargedUpManaConsumption * Time.fixedDeltaTime;
                }
            }
            else
            {
                CancelCharging();
            }
        }
        if (!animator.GetBool("isBlocking") && !isChargedUp)
        {
            if (mana <= 100f)
            {
                mana += 10f * Time.fixedDeltaTime;
            }
        }
    }

    // This method resets the pitch
    public void playSound(AudioClip audio)
    {
        // audioSource.Stop();
        audioSourceCombat.pitch = Random.Range(1f, 1.5f);
        audioSourceCombat.PlayOneShot(audio);
    }

    public void playSoundMovement(AudioClip audio)
    {
        audioSourceCombat.pitch = 1f;
        audioSourceCombat.PlayOneShot(audio);
    }

    public void attack(float damage)
    {
        CancelCharging();
        if (!animator.GetBool("isBlocking"))
        {
            knockBackCounter = knockBackTotalTime;
            health -= damage;
            animator.Play("Attacked");
        }
        else
        {
            animator.SetTrigger("Attacked");
            // isBlocking = false;
        }

    }

    public void spawnMagicDagger()
    {
        // hitArea.spawnMagicDagger();
        if (mana - 10f > 0)
        {
            mana -= 10f;
            Vector3 size = Vector3.zero;
            if (chargingEnergy > 0)
            {
                if (chargingEnergy > 3) size = new Vector3(3, 3, 0);
                else size = new Vector3(chargingEnergy, chargingEnergy, 0);
            }
            if (transform.localScale.x == 1)
            {

                magicAttackSpawner.spawnMagicDagger("Right",size,chargingEnergy);
            }

            if (transform.localScale.x == -1)
            {
                magicAttackSpawner.spawnMagicDagger("Left",size,chargingEnergy);
            }
        }

        isChargedUp = false;
        chargingEnergy = 0f;
    }

    void CancelCharging()
    {
        if (isChargedUp)
        {
            isChargedUp = false;
            chargingEnergy = 0f;
            animator.SetBool("ChargedUp", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 contactPoint = contact.point;
                Vector2 center = transform.position;

                float direction = contactPoint.x - center.x;

                if (direction < 0)
                {
                    Debug.Log("Hit from the LEFT");
                    isWalledLeft = true;
                }
                else if (direction > 0)
                {
                    Debug.Log("Hit from the RIGHT");
                    isWalledRight = true;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWalledLeft = false;
            isWalledRight = false;
        }
    }
}
