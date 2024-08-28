using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    void SetVelocityForward(Rigidbody2D bulletRigidbody2D, Transform transform, float speed);
}
