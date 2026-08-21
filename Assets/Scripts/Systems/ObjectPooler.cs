using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class ObjectToPool
    {
        public GameObject objPrefab;
        public int numberToPool;
        public string objectID;
    }

    public ObjectToPool[] objectsToPool;
    public GameObject poolParent;
    public GameObject poolablesInUse;

    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, Transform> poolParents = new Dictionary<string, Transform>();
    private Dictionary<string, GameObject> prefabsByID = new Dictionary<string, GameObject>();

    private void Start()
    {
        foreach (ObjectToPool obj in objectsToPool)
        {
            obj.objectID = obj.objPrefab.GetComponent<ObjectID>().objID;
            CreatePool(obj);
        }
    }

    private void CreatePool(ObjectToPool obj)
    {
        GameObject poolGO = new GameObject(obj.objectID + " Pool", typeof(Transform));
        poolGO.transform.SetParent(poolParent.transform, false);
        poolGO.AddComponent<ObjectID>().objID = obj.objectID;

        poolParents[obj.objectID] = poolGO.transform;
        prefabsByID[obj.objectID] = obj.objPrefab;

        Queue<GameObject> queue = new Queue<GameObject>();
        for (int i = 0; i < obj.numberToPool; i++)
        {
            GameObject pooledObj = Instantiate(obj.objPrefab, poolGO.transform);
            pooledObj.SetActive(false);

            ObjectID id = pooledObj.GetComponent<ObjectID>();
            id.objID = obj.objectID;
            id.isPooled = true;

            queue.Enqueue(pooledObj);
        }

        pools[obj.objectID] = queue;
    }

    public GameObject SpawnPooledObject(GameObject objToSpawn)
    {
        ObjectID sourceId = objToSpawn.GetComponent<ObjectID>();
        string itemID = sourceId.objID;

        if (!pools.TryGetValue(itemID, out Queue<GameObject> queue))
        {
            Debug.LogWarning($"No pool found for Object ID: '{itemID}'");
            return null;
        }

        GameObject obj;
        if (queue.Count > 0)
        {
            obj = queue.Dequeue();
        }
        else
        {
            obj = Instantiate(prefabsByID[itemID], poolParents[itemID]);
            obj.GetComponent<ObjectID>().objID = itemID;
        }

        ObjectID spawnedId = obj.GetComponent<ObjectID>();
        spawnedId.isPooled = false;

        obj.transform.SetParent(poolablesInUse.transform, false);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObjectToPool(GameObject objToReturn)
    {
        ObjectID id = objToReturn.GetComponent<ObjectID>();
        string itemID = id.objID;

        if (id.isPooled)
        {
            Debug.LogWarning($"Object '{objToReturn.name}' (ID: '{itemID}') is already pooled — ignoring duplicate return.");
            return;
        }

        if (!pools.TryGetValue(itemID, out Queue<GameObject> queue) ||
            !poolParents.TryGetValue(itemID, out Transform parent))
        {
            Debug.LogWarning($"No pool found for Object ID: '{itemID}'");
            return;
        }

        id.isPooled = true;
        objToReturn.SetActive(false);
        objToReturn.transform.SetParent(parent, false);
        queue.Enqueue(objToReturn);
    }
}