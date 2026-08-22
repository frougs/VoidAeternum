using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class ShipVisualUpdater : MonoBehaviour
{

    [SerializeField] private GameObject lWing;
    [SerializeField] private GameObject rWing;
    [SerializeField] private GameObject hull;
    [SerializeField] private GameObject engine;

    public Dictionary<string, GameObject> shipVisuals = new Dictionary<string, GameObject>();

    public UnityEvent updatedShipVisuals;

    [SerializeField] bool updateWeaponSlots = false;

    [SerializeField] BaseWing[] wings;

    public void Start()
    {
        shipVisuals.Add("lWing", lWing);
        shipVisuals.Add("rWing", rWing);
        shipVisuals.Add("hull", hull);
        shipVisuals.Add("engine", engine);
    }

    public void UpdateLWing(GameObject newLWing)
    {
        lWing = newLWing;
        shipVisuals["lWing"] = lWing;
        ReplaceShipPart(newLWing, lWing);
        updatedShipVisuals.Invoke();
    }

    public void UpdateRWing(GameObject newRWing)
    {
        rWing = newRWing;
        shipVisuals["rWing"] = rWing;
        updatedShipVisuals.Invoke();
    }

    public void UpdateHull(GameObject newHull)
    {
        hull = newHull;
        shipVisuals["hull"] = hull;
        updatedShipVisuals.Invoke();
    }

    public void UpdateEngine(GameObject newEngine)
    {
        engine = newEngine;
        shipVisuals["engine"] = engine;
        updatedShipVisuals.Invoke();
    }

    public Dictionary<string, GameObject> GetCurrentVisuals()
    {
        return shipVisuals;
    }

    private void ReplaceShipPart(GameObject newPart, GameObject oldPart)
    {
        Vector3 position = oldPart.transform.position;
        Quaternion rotation = oldPart.transform.rotation;
        Transform parent = oldPart.transform.parent;
        GameObject replacedPart = Instantiate(newPart, position, rotation, parent);
        Destroy(oldPart);
    }

    public void Update()
    {
        if (updateWeaponSlots)
        {
            foreach(BaseWing wing in wings)
            {
                wing.Refresh();
            }
            updateWeaponSlots = false;
        }
    }
}
