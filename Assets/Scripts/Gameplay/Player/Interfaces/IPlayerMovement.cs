using UnityEngine;

public interface IPlayerMovement
{
    public void MovePlayer(UnityEngine.Vector2 movement, float speed, Rigidbody2D rigidbody);
}
