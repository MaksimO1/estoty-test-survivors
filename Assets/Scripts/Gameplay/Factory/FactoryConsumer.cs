using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryConsumer : MonoBehaviour
{
    private BulletSpawner _bulletSpawner;
    private EnemySpawner _enemySpawner;
    private ItemSpawner _itemSpawner;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Collider2D _boundCollider;
    [SerializeField]
    private List<GameObject> _itemPrefabs;
    [SerializeField]
    private List<GameObject> _enemyPrefabs;
    [SerializeField]
    private Transform _itemParentTransform;
    [SerializeField]
    private Transform _enemyParentTransform;
    [SerializeField]
    private float _enemySpawnPeriod;
    [SerializeField]
    private float _minEnemySpawnPeriod;
    [SerializeField]
    private float _enemySpawnPeriodIncrease;
    [SerializeField]
    private float _enemySpawnPeriodIncreasePeriod;
    private bool _cooldown = false;
    private bool _spawnPeriodCooldown = false;

    [Inject]
    public void Constructor(BulletSpawner bulletSpawner, EnemySpawner enemySpawner, ItemSpawner itemSpawner)
    {
        _bulletSpawner = bulletSpawner;
        _enemySpawner = enemySpawner;
        _itemSpawner = itemSpawner;
    }

    void FinishSpawnPeriodCooldown()
    {
        _spawnPeriodCooldown = false;
    }

    void FinishCooldown()
    {
        _cooldown = false;
    }

    void DecreaseSpawnPeriod()
    {
        _enemySpawnPeriod = Math.Clamp(_enemySpawnPeriod - _enemySpawnPeriodIncrease, _minEnemySpawnPeriod, _enemySpawnPeriod);
    }

    Vector2 GenerateRandomSpawnPosition()
    {
        var posGenerator = new RandomOfScrenPosGenerator(_camera, _boundCollider.bounds);
        Vector2 spawnPosition = posGenerator.FindSpawnLocationOutsideScreen();
        return spawnPosition;
    }

    void FixedUpdate()
    {
        if (!_cooldown)
        {
            _cooldown = true;
            SpawnEnemy(GenerateRandomSpawnPosition());
            Invoke(nameof(FinishCooldown), _enemySpawnPeriod);
        }
        if (!_spawnPeriodCooldown)
        {
            _spawnPeriodCooldown = true;
            DecreaseSpawnPeriod();
            Invoke(nameof(FinishSpawnPeriodCooldown), _enemySpawnPeriodIncreasePeriod);
        }
    }

    public void SpawnBullet(Quaternion rotation, Vector2 position)
    {
        _bulletSpawner.SpawnBullet(position, rotation);
    }

    public void SpawnItem(Vector2 position, ItemTypeEnum itemType)
    {

        _itemSpawner.SpawnItem(position, _itemPrefabs[(int)itemType], _itemParentTransform);
    }

    public void SpawnEnemy(Vector2 position)
    {
        _enemySpawner.SpawnEnemy(position, _enemyPrefabs[UnityEngine.Random.Range(0, _enemyPrefabs.Count)], _enemyParentTransform);
    }

}
