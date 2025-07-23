using UnityEngine;

public class DontDestroyGameObjScript : MonoBehaviour
{
    public static DontDestroyGameObjScript instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
