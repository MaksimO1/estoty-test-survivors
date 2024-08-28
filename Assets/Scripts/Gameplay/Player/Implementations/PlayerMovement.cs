using System.Numerics;
using UnityEngine;

public class PlayerMovement : IPlayerMovement
{
    public void MovePlayer(UnityEngine.Vector2 movement, float speed, Rigidbody2D rigidbody)
    {
        rigidbody.velocity = movement * speed;
    }
}
