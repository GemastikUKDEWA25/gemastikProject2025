using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public GolemScript golemHurtBox;
    public bool isCore = false;

    public void attack(float damage)
    {
        if (isCore) golemHurtBox.attack(damage);
    }
}
