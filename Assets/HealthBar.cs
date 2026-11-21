using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // need this for slider
using TMPro; // need this for text mesh pro

public class HealthBar : MonoBehaviour
{
    public Slider healthBarSlider;
    public TextMeshProUGUI healthBarValueText; // the text that says 10/10

    public int maxHealth; // maximum health
    public int currHealth; // current health

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth; // set the current health to max health
    }

    // Update is called once per frame
    void Update()
    {
        // set the health bar text
        if (healthBarValueText != null)
        {
            healthBarValueText.text = currHealth.ToString() + "/" + maxHealth.ToString();
        }

        //set the slider values
        if (healthBarSlider != null)
        {
            healthBarSlider.value = currHealth;
            healthBarSlider.maxValue = maxHealth;
        }
    }
}