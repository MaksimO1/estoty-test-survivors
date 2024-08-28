using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyMovementConsumer : MonoBehaviour
{
    private IEnemyMovement _iEnemyMovement;
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private float _enemySpeed;
    [Inject]
    public void Construct(IEnemyMovement iEnemyMovement)
    {
        _iEnemyMovement = iEnemyMovement;
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _iEnemyMovement.MoveTowardPlayer(_rigidbody2D, gameObject.transform.position, ObtainPlayerPosition(), _enemySpeed);
    }

    Vector2 ObtainPlayerPosition()
    {
        return GameObject.FindWithTag("Player").transform.position;
    }
}
