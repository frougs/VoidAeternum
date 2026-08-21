using UnityEngine;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
    public int money;
    public UnityEvent<int> updateMoney;
    public void CollectMoney(int amt)
    {
        money += 1;
        money = Mathf.Clamp(money, 0, 999);
        updateMoney.Invoke(money);
    }

    public void RemoveMoney(int amt)
    {
        money -= 1;
        money = Mathf.Clamp(money, 0, 999);
        updateMoney.Invoke(money);
    }
}
