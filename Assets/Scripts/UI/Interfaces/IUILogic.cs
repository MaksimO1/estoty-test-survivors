using UnityEngine;
using UnityEngine.UI;

public interface IUILogic
{
    void UpdateKills(int kills, Text killText);
    void UpdateAmmo(int maxAmmo, int currentAmmo, int stashedAmmo, Text ammoText);
    void UpdateHealth(int currentHealth, int maxHealth, Slider healthSlider);
    void UpdateExperienceText(int level, Text experienceText);
    void UpdateExperienceSlider(int currentExperienceAdjusted, int nextLevelExperienceCap, Slider experienceSlider);
    void DisplayDeath(GameObject deathScreen);
}
