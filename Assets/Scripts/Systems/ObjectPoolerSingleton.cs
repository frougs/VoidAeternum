using UnityEngine;

public class ObjectPoolerSingleton : MonoBehaviour
{
    public static ObjectPoolerSingleton instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
