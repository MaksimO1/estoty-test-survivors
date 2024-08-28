using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class EnemyAttacks : IEnemyAttacks
{
    public void ConductPlayerAttack(SignalBus bus, int damage)
    {
        bus.Fire(new EnemyCollisionSignal { collisionDamage = damage });
    }
}
