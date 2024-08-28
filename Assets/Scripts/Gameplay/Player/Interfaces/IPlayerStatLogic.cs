using UnityEngine;
using System.Collections.Generic;

public interface IPlayerStatLogic
{
    public int CalculateCurrentPlayerHealth(int healthAdjustment, int maxHealth, int currentHealth);
    public int CalculateCurrentExperience(int gainedExperience, int currentExperience);
    public int CalculateExperienceCap(int currentLevel, int experienceNeededPerLevel);
    public int CalculateLevel(int currentExperience, int experienceNeededPerLevel);
    public int CalculateNewKillCount(int currentKillCount, int adjustment);
    public bool CheckForDeath(int currentHealth);
}
