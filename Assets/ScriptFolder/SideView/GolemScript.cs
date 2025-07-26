using UnityEngine;
using TMPro;

public class GolemScript : MonoBehaviour
{
    float health = 100;
    public static EnemyScript Instance { get; private set; }
    public TextMeshProUGUI healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        healthText.text = health.ToString();
    }

    public void attack(float damage)
    {
        health -= damage;
    }
    public float getHealth()
    {
        return health;
    }
}
