using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyAttackConsumer : MonoBehaviour
{
    private IEnemyAttacks _iEnemyAttack;
    private SignalBus _signalBus;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _damagePeriod;
    private bool _damageCooldown = false;
    private bool _inCollision = false;
    [Inject]
    public void Construct(IEnemyAttacks iEnemyAttack, SignalBus signalBus)
    {
        _iEnemyAttack = iEnemyAttack;
        _signalBus = signalBus;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void AttackPlayer()
    {
        if (_inCollision)
        {
            _damageCooldown = true;
            Invoke(nameof(FinishCooldown), _damagePeriod);
            _iEnemyAttack.ConductPlayerAttack(_signalBus, _damage);
            Invoke(nameof(AttackPlayer), _damagePeriod);
        }
    }

    void FinishCooldown()
    {
        _damageCooldown = false;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            _inCollision = true;
            if (!_damageCooldown)
            {
                AttackPlayer();
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            _inCollision = false;
        }
    }

}
