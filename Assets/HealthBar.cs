using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthText; 

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        UpdateText(health, health);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        UpdateText(health, (int)slider.maxValue);
    }

    private void UpdateText(int current, int max)
    {
        if (healthText != null)
        {
            healthText.text = current + " / " + max;
        }
    }
}