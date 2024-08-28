using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemSpawner
{
    readonly DiContainer _container;

    public ItemSpawner(DiContainer container)
    {
        _container = container;
    }

    public void SpawnItem(Vector2 position, GameObject prefab, Transform itemParent)
    {
        _container.InstantiatePrefabForComponent<ItemLogicConsumer>(prefab, position, itemParent.rotation, itemParent);
    }
}
