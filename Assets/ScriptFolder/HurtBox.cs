using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public GolemScript golemHurtBox;
    public bool isCore = false;

    public void attack(float damage)
    {
        golemHurtBox.attack(damage);
    }
}
