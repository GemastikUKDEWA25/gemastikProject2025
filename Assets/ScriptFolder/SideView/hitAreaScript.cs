using UnityEngine;

public class hitAreaScript : MonoBehaviour
{
    CircleCollider2D circleCollider;
    Vector2 circleOffset;
    public static hitAreaScript Instance { get; private set; }
    EnemyScript overlappingEnemy;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleOffset = circleCollider.offset;
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.J) && overlappingEnemy != null)
            {   
                overlappingEnemy.attack(10);
                Debug.Log(overlappingEnemy.getHealth());
            }
    }

    public void flipCircleOffset()
    {
        circleOffset.x *= -1;
        circleCollider.offset = circleOffset;
    }
    public Vector2 getCircleOffset()
    {
        return circleOffset;
    }
    public CircleCollider2D getCircleCollider2d()
    {
        return circleCollider;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            overlappingEnemy = collision.GetComponent<EnemyScript>();
            // if (Input.GetKeyDown(KeyCode.J))
            // {
            //     enemy.attack(10);
            //     Debug.Log(enemy.getHealth());
            // }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && overlappingEnemy != null && collision.gameObject == overlappingEnemy.gameObject)
        {
            overlappingEnemy = null;
        }
    }
}
