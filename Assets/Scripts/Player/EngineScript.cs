using UnityEngine;

public class EngineScript : MonoBehaviour
{
    [SerializeField] float engineEfficiency; //lower numba is higher effiecieny
    [SerializeField] float engineThrust; //how fast the engine allows you to go
    public float currentFuel;
    private PlayerStats player;

    private void OnEnable()
    {
        player = GetComponentInParent<PlayerStats>();
        currentFuel = player.maxFuel;
        player.playerThrust = engineThrust;
    }

    private void Update()
    {
        currentFuel = currentFuel - ((1 * engineEfficiency) * Time.deltaTime);
        player.UpdateFuel(currentFuel);
    }
    public void AddFuel(float amt)
    {
        Debug.Log("Adding Fuel: " +amt);
        currentFuel += amt;
        currentFuel = Mathf.Clamp(currentFuel, 0, player.maxFuel);
    }
    public void Refill()
    {
        AddFuel(player.maxFuel);
    }


}
