using UnityEngine;
using UnityEngine.UI;

public class UILogic : IUILogic
{
    public void DisplayDeath(GameObject deathScreen)
    {
        deathScreen.SetActive(true);
    }

    public void UpdateAmmo(int maxAmmo, int currentAmmo, int stashedAmmo, Text ammoText)
    {
        ammoText.text = "Ammo: " + currentAmmo.ToString() + "/" + maxAmmo.ToString() + ", " + stashedAmmo.ToString();
    }

    public void UpdateExperienceSlider(int currentExperienceAdjusted, int nextLevelExperienceCap, Slider experienceSlider)
    {
        experienceSlider.value = (float)currentExperienceAdjusted / (float)nextLevelExperienceCap;
    }

    public void UpdateExperienceText(int level, Text experienceText)
    {
        experienceText.text = "Lv." + level.ToString();
    }

    public void UpdateHealth(int currentHealth, int maxHealth, Slider healthSlider)
    {
        healthSlider.value = (float)currentHealth / (float)maxHealth;
    }

    public void UpdateKills(int kills, Text killText)
    {
        killText.text = "" + kills.ToString();
    }
}
