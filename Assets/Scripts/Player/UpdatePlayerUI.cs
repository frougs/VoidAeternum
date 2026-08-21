using UnityEngine;
using System.Collections;
using TMPro;

public class UpdatePlayerUI : MonoBehaviour
{
    [Header("Health Stuff")]
    private float lastHealth;
    private float lastMaxHealth;
    private float lastHealthFillAmount;
    [SerializeField] RectTransform delayedHealthBar;
    [SerializeField] RectTransform healthBar;
    [SerializeField] float delayedSpeed;
    float maxBarWidth = 100;
    private Coroutine updateHealthBar;

    [Header("Exp Stuff")]
    [SerializeField] RectTransform expBar;
    [SerializeField] TextMeshProUGUI levelText;
    [Header("Money Stuff")]
    [SerializeField] TextMeshProUGUI moneyText;

    [Header("Fuel Stuff")]
    [SerializeField] RectTransform fuelBar;

    public void UpdateHealthUI(float oldHealth, float currentHealth, float maxHealth)
    {
        float newFillAmount = currentHealth / maxHealth;
        float newWidth = maxBarWidth * newFillAmount;

        if (currentHealth != lastHealth || maxHealth != lastMaxHealth || Mathf.Abs(delayedHealthBar.sizeDelta.x - newWidth) > Mathf.Epsilon)
        {
            healthBar.sizeDelta = new Vector2(newWidth, healthBar.sizeDelta.y);

            // Cancel any in-progress catch-up animation before starting a new one
            if (updateHealthBar != null)
            {
                StopCoroutine(updateHealthBar);
            }
            updateHealthBar = StartCoroutine(DelayedHealthBarUpdate(newWidth));

            lastHealth = currentHealth;
            lastMaxHealth = maxHealth;
            lastHealthFillAmount = newFillAmount;
        }
    }

    private IEnumerator DelayedHealthBarUpdate(float targetWidth)
    {
        while (Mathf.Abs(delayedHealthBar.sizeDelta.x - targetWidth) > Mathf.Epsilon)
        {
            float newWidth = Mathf.Lerp(delayedHealthBar.sizeDelta.x, targetWidth, Time.deltaTime * delayedSpeed);
            delayedHealthBar.sizeDelta = new Vector2(newWidth, delayedHealthBar.sizeDelta.y);
            yield return null;
        }

        delayedHealthBar.sizeDelta = new Vector2(targetWidth, delayedHealthBar.sizeDelta.y);
        updateHealthBar = null;
    }

    public void UpdateFuelUI(float currentFuel, float maxFuel)
    {
        float newFillAmount = (currentFuel / maxFuel) * maxBarWidth;
        fuelBar.sizeDelta = new Vector2(newFillAmount, fuelBar.sizeDelta.y);
    }

    public void UpdateExpUI(float currentExp, float expToLevelUp)
    {
        float newFillAmount = (currentExp / expToLevelUp) * maxBarWidth;
        expBar.sizeDelta = new Vector2(newFillAmount, expBar.sizeDelta.y);
    }

    public void UpdateLevelUI(int level)
    {
        levelText.text = level.ToString();
    }
    public void UpdateMoneyUI(int money)
    {
        moneyText.text = "$" +money.ToString();
    }
}