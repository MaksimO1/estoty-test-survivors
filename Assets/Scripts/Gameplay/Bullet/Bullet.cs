using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : IBullet
{
    public void SetVelocityForward(Rigidbody2D bulletRigidbody2D, Transform transform, float speed)
    {
        bulletRigidbody2D.velocity = transform.up * speed;
    }

}
