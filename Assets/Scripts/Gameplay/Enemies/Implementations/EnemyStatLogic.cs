using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatLogic : IEnemyStatLogic
{
    public int CalculateNewHealth(int currentHealth, int healthAdjustment)
    {
        return currentHealth+healthAdjustment;
    }
}
