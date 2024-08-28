using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner
{
    readonly DiContainer _container;

    public EnemySpawner(DiContainer container)
    {
        _container = container;
    }

    public void SpawnEnemy(Vector2 position, GameObject prefab, Transform itemParent)
    {
        _container.InstantiatePrefab(prefab, position, itemParent.rotation, itemParent);
    }
}
