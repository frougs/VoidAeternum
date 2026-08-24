using UnityEngine;
using UnityEngine.Events;
public class ExpManager : MonoBehaviour
{
    public UnityEvent<float, float> updatedExp;
    public UnityEvent<int, int> leveledUp;
    private float curExp;
    [SerializeField] private float expToNextLevel;
    public int curLevel= 0;
    [SerializeField] private float levelMulti;
    [SerializeField] private bool forcedLevelUp;
    public void AddExp(float amnt)
    {
        curExp += amnt * this.GetComponent<PlayerStats>().expMultiplier;
        updatedExp.Invoke(curExp, expToNextLevel);
        CheckLevel();
    }

private void CheckLevel()
{
    if (curExp >= expToNextLevel)
    {
        curExp -= expToNextLevel;   
        LevelUp();
    }
}

public void LevelUp()
{
    curLevel += 1;
    expToNextLevel *= levelMulti;   
    //Level Up Behavior
    leveledUp.Invoke(curLevel, this.GetComponent<PlayerStats>().numOfUpgradeChoices);
    CheckLevel();                   
}
    private void Update()
    {
        if (forcedLevelUp)
        {
            forcedLevelUp = false;
            curExp = expToNextLevel;
            CheckLevel();
        }
    }
}
