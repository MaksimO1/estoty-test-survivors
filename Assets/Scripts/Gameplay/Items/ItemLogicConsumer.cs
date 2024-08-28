using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class ItemLogicConsumer : MonoBehaviour
{
    private SignalBus _signalBus;
    private IItemLogic _iItemLogic;
    private Transform _player;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _attractionDistance;
    [SerializeField] private float _speed;
    [SerializeField] private int _itemValue;
    [SerializeField] private ItemTypeEnum _itemTypeEnum;
    [Inject]
    public void Construct(IItemLogic iItemLogic, SignalBus signalBus)
    {
        _iItemLogic = iItemLogic;
        _signalBus = signalBus;
    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (_iItemLogic.InAttractionRange(_player.position, transform.position, _attractionDistance))
        {
            _iItemLogic.MoveTowardNearbyPlayer(_player.position, transform.position, _attractionDistance, _speed, _rigidbody2D);
        }
    }

    public void ConsumeItem()
    {
        _iItemLogic.ConsumeItem(_signalBus, _itemTypeEnum, _itemValue);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ConsumeItem();
        }
    }
}
