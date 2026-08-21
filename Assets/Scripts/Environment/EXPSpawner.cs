using UnityEngine;

public class EXPSpawner : MonoBehaviour
{
    [SerializeField] bool useBaseValues;
    [SerializeField] int baseMinExp;
    [SerializeField] int baseMaxExp;
    [SerializeField] GameObject exp;
    public void SpawnEXP(int minExp, int maxExp)
    {
        if(!useBaseValues){
           RollEXP(minExp, maxExp);
        }
        else
        {
            RollEXP(baseMinExp, baseMaxExp);
        }
    }
    private void RollEXP(int minExp, int maxExp)
    {
         int amountToSpawn = Random.Range(minExp, maxExp);
            for(int i=0; i< amountToSpawn; i++)
            {
                //Instantiate(exp, this.transform.position, Quaternion.identity);
                GameObject spawnedEXP = ObjectPoolerSingleton.instance.GetComponent<ObjectPooler>().SpawnPooledObject(exp);
                spawnedEXP.transform.position = this.transform.position;
            }
    }
}
