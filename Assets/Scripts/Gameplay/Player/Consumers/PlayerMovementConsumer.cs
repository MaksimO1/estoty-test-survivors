using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMovementConsumer : MonoBehaviour
{
    private IPlayerMovement _iPlayerMovement;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    [SerializeField] private InputActionReference _inputActionReference;

    [Inject]
    public void Construct(IPlayerMovement iPlayerMovement)
    {
        _iPlayerMovement = iPlayerMovement;
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = _inputActionReference.action.ReadValue<Vector2>();
        _iPlayerMovement.MovePlayer(movement, _speed, _rigidbody2D);
    }
}
