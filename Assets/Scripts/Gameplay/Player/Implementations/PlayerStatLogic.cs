using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

public class PlayerStatLogic : IPlayerStatLogic
{
    public int CalculateCurrentPlayerHealth(int healthAdjustment, int maxHealth, int currentHealth)
    {
        currentHealth += healthAdjustment;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        return currentHealth;
    }

    public int CalculateExperienceCap(int currentLevel, int experienceNeededPerLevel)
    {
        return currentLevel * experienceNeededPerLevel;
    }

    public int CalculateLevel(int currentExperience, int experienceNeededPerLevel)
    {
        return 1 + (int)((float)currentExperience / (float)experienceNeededPerLevel);
    }

    public bool CheckForDeath(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    public int CalculateCurrentExperience(int gainedExperience, int currentExperience)
    {
        currentExperience += gainedExperience;
        return currentExperience;
    }

    public int CalculateNewKillCount(int currentKillCount, int adjustment)
    {
        return currentKillCount + adjustment;
    }
}
