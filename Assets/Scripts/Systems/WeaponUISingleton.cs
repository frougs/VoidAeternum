using UnityEngine;

public class WeaponUISingleton : MonoBehaviour
{
    public static WeaponUISingleton instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
