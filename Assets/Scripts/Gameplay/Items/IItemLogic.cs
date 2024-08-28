using System;
using UnityEngine;
using Zenject;

public interface IItemLogic
{
    public void ConsumeItem(SignalBus signalBus, ItemTypeEnum itemType, int itemValue);
    public bool InAttractionRange(Vector2 playerPosition, Vector2 itemPosition, float range);
    public void MoveTowardNearbyPlayer(Vector2 playerPosition, Vector2 itemPosition, float range, float speed, Rigidbody2D rigidbody2D);
}
