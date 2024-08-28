using UnityEngine;
using Zenject;

public class ItemLogic : IItemLogic
{
    public void ConsumeItem(SignalBus signalBus, ItemTypeEnum itemType, int itemValue)
    {
        switch (itemType)
        {
            case ItemTypeEnum.Heal:
                signalBus.Fire(new PlayerHealingItemConsumptionSignal { healthChange = itemValue });
                break;
            case ItemTypeEnum.Experience:
                signalBus.Fire(new PlayerExperienceItemConsumptionSignal { experienceChange = itemValue });
                break;
            case ItemTypeEnum.Ammo:
                signalBus.Fire(new PlayerAmmoItemConsumptionSignal { ammoChange = itemValue });
                break;
        }
    }

    public bool InAttractionRange(Vector2 playerPosition, Vector2 itemPosition, float range)
    {
        Vector2 directionToTarget = playerPosition - itemPosition;
        float sqrTarget = directionToTarget.sqrMagnitude;
        if (range >= sqrTarget)
        {
            return true;
        }
        return false;
    }

    public void MoveTowardNearbyPlayer(Vector2 playerPosition, Vector2 itemPosition, float range, float speed, Rigidbody2D rigidbody2D)
    {
        Vector2 direction = (playerPosition - itemPosition).normalized;

        rigidbody2D.MovePosition(itemPosition + direction * speed * Time.fixedDeltaTime);
    }
}
