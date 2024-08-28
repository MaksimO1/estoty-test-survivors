using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyStatLogicConsumer : MonoBehaviour
{
    private IEnemyStatLogic _iEnemyStatLogic;
    private SignalBus _signalBus;
    [Min(1)]
    [SerializeField]
    private int _enemyHealth;
    [Inject]
    public void Construct(IEnemyStatLogic iEnemyStatLogic, SignalBus signalBus)
    {
        _iEnemyStatLogic = iEnemyStatLogic;
        _signalBus = signalBus;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            _enemyHealth = _iEnemyStatLogic.CalculateNewHealth(_enemyHealth, -1);
            Destroy(other.gameObject);
            if (_enemyHealth <= 0)
            {
                _signalBus.Fire(new EnemyDeathSignal());
                ItemTypeEnum itemType = (ItemTypeEnum)Random.Range(0,2);
                _signalBus.Fire(new EnemySpawnItemSignal { spawnLocation = transform.position, itemTypeEnum = itemType });
                _signalBus.Fire(new EnemySpawnItemSignal { spawnLocation = transform.position, itemTypeEnum = (ItemTypeEnum)2 });
                Destroy(gameObject);
            }
        }
    }
}
