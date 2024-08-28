using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : IEnemyMovement
{
    public void MoveTowardPlayer(Rigidbody2D rigidbody, Vector2 enemyPosition, Vector2 playerPosition, float speed)
    {
        Vector2 direction = (playerPosition - enemyPosition).normalized;
        
        rigidbody.MovePosition(enemyPosition + direction * speed * Time.fixedDeltaTime);
    }
    
}
