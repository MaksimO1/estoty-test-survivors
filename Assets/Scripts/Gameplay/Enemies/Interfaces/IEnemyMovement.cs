using UnityEngine;

public interface IEnemyMovement
{
public void MoveTowardPlayer(Rigidbody2D rigidbody, Vector2 enemyPosition, Vector2 playerPosition, float speed);
}
