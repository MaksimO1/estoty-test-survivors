using System.IO;
using UnityEngine;
using System.Collections.Generic;

public interface IPlayerAttacks
{
    void RotateTowardsClosestEnemy(Transform weaponTransform, Vector2 targetPosition, float weaponRotationSpeed);
    Vector2 FindClosestEnemyPosition(Transform playerTransform, GameObject[] enemies, float range);
    bool CanShoot(bool onCooldown, int currentAmmo);
    int CalculateNewStashedAmmo(int stashedAmmo, int ammoAdjustment);
    public int CalculateAmmoAfterReload(int stashedAmmo, int ammoReloaded, int maxAmmo);
    int CalculateReloadedAmmo(int stashedAmmo, int currentAmmo, int maxAmmo);
}
