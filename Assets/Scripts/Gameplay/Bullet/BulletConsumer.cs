using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletConsumer : MonoBehaviour
{
    private IBullet _iBullet;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float destructionDelay;

    [Inject]
    public void Consturctor(IBullet iBullet, Vector2 position, Quaternion rotation)
    {
        _iBullet = iBullet;
        transform.position = position;
        transform.rotation = rotation;
    }

    void Start()
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        Invoke(nameof(SelfDestruct), destructionDelay);
        _iBullet.SetVelocityForward(rigidbody2D, transform, speed);
    }
    void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public class Factory : PlaceholderFactory<Vector2, Quaternion, BulletConsumer>
    {
    }
}
