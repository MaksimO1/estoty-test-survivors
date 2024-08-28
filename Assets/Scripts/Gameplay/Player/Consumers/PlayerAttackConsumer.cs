using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAttackConsumer : MonoBehaviour
{
    private IPlayerAttacks _iPlayerAttacks;
    private SignalBus _signalBus;
    [SerializeField]
    private float _weaponCooldown;
    [SerializeField]
    private float _weaponReloadCooldown;
    [SerializeField]
    private float _weaponRange;
    [SerializeField]
    private float _weaponRotationSpeed;
    [SerializeField]
    private int _stashedAmmo;
    [SerializeField]
    private int _maxAmmo;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private Transform weaponTransform;
    [SerializeField]
    private Transform playerTransform;
    private int _currentAmmo;
    private bool _onCooldown;

    [Inject]
    public void Construct(IPlayerAttacks iPlayerAttacks, SignalBus signalBus)
    {
        _signalBus = signalBus;
        _iPlayerAttacks = iPlayerAttacks;
    }
    // Start is called before the first frame update

    void Start()
    {
        _onCooldown = false;
        Reload();
    }

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyList != default)
        {
            Vector2 closestEnemyPosition = _iPlayerAttacks.FindClosestEnemyPosition(playerTransform, enemyList, _weaponRange);
            if (closestEnemyPosition != default)
            {
                _iPlayerAttacks.RotateTowardsClosestEnemy(weaponTransform, closestEnemyPosition, _weaponRotationSpeed);
                ShootBullet();
            }
        }
    }

    public void GainAmmo(int gainedAmmo)
    {
        _stashedAmmo = _iPlayerAttacks.CalculateNewStashedAmmo(_stashedAmmo, gainedAmmo);
        UpdateAmmoUI();
    }

    void Reload()
    {
        int reloadedAmmo = _iPlayerAttacks.CalculateReloadedAmmo(_stashedAmmo, _currentAmmo, _maxAmmo);
        int futureCurrentAmmo = _iPlayerAttacks.CalculateAmmoAfterReload(_stashedAmmo, reloadedAmmo, _maxAmmo);
        _stashedAmmo = _iPlayerAttacks.CalculateNewStashedAmmo(_stashedAmmo, -reloadedAmmo);
        _currentAmmo = futureCurrentAmmo;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        _signalBus.Fire(new PlayerAmmoChangeSignal { currentAmmo = _currentAmmo, maxAmmo = _maxAmmo, stashedAmmo = _stashedAmmo });
    }

    void FinishCooldown()
    {
        _onCooldown = false;
    }

    void SpawnBullet()
    {
        Quaternion bulletRotation = Quaternion.Euler(0, 0, weaponTransform.rotation.eulerAngles.z - 90.0f); ;
        _signalBus.Fire(new PlayerSpawnBulletSignal { rotation = bulletRotation, spawnLocation = weaponTransform.transform.position });
    }

    void CooldownWeapon()
    {
        if (_currentAmmo != 0)
        {
            Invoke(nameof(FinishCooldown), _weaponCooldown);
        }
        else
        {
            Invoke(nameof(Reload), _weaponReloadCooldown);
            Invoke(nameof(FinishCooldown), _weaponReloadCooldown);
        }
    }

    public void ShootBullet()
    {
        if (_currentAmmo > 0 && !_onCooldown)
        {
            SpawnBullet();
            _onCooldown = true;
            _currentAmmo--;
            UpdateAmmoUI();
            CooldownWeapon();
        }
    }
}
