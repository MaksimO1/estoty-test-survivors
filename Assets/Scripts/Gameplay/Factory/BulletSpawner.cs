using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletSpawner
{
    private readonly BulletConsumer.Factory _bulletFactory;

    public BulletSpawner(BulletConsumer.Factory bulletFactory)
    {
        _bulletFactory = bulletFactory;
    }

    public void SpawnBullet(Vector2 position, Quaternion rotation)
    {
        BulletConsumer bullet = _bulletFactory.Create(position, rotation);
    }
}
