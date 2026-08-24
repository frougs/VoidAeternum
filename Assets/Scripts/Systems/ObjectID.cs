using UnityEngine;

public class ObjectID : MonoBehaviour
{
    [SerializeField] public string objID;
    public string itemName;
    public string location;
    public string equipable;
    [HideInInspector] public bool isPooled;
}
