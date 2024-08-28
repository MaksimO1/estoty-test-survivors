using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UILogicConsumer : MonoBehaviour
{
    private IUILogic _iUILogic;
    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private Slider _experienceSlider;
    [SerializeField]
    private GameObject _deathScreen;
    [SerializeField]
    private Text _ammoIndicator;
    [SerializeField]
    private Text _levelIndicator;
    [SerializeField]
    private Text _killCounter;

    [Inject]
    public void Construct(IUILogic iUILogic)
    {
        _iUILogic = iUILogic;
    }

    public void TriggerDeathScreen()
    {
        _iUILogic.DisplayDeath(_deathScreen);
    }

    public void UpdateKillCountUI(int killCount)
    {
        _iUILogic.UpdateKills(killCount, _killCounter);
    }

    public void UpdateAmmoUI(int currentAmmo, int maxAmmo, int stashedAmmo)
    {
        _iUILogic.UpdateAmmo(maxAmmo, currentAmmo, stashedAmmo, _ammoIndicator);
    }

    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        _iUILogic.UpdateHealth(currentHealth, maxHealth, _healthSlider);
    }

    public void UpdateExperienceUI(int currentExperienceAdjusted, int nextLevelExperienceCap, int level)
    {
        _iUILogic.UpdateExperienceText(level, _levelIndicator);
        _iUILogic.UpdateExperienceSlider(currentExperienceAdjusted, nextLevelExperienceCap, _experienceSlider);
    }
}
