using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerAttacks : IPlayerAttacks
{
    public int CalculateNewStashedAmmo(int stashedAmmo, int ammoAdjustment)
    {
        return stashedAmmo + ammoAdjustment;
    }

    public int CalculateAmmoAfterReload(int stashedAmmo, int ammoReloaded, int maxAmmo)
    {
        return Math.Clamp(stashedAmmo - ammoReloaded, 0, maxAmmo);
    }

    public int CalculateReloadedAmmo(int stashedAmmo, int currentAmmo, int maxAmmo)
    {
        return Math.Clamp(-(currentAmmo - maxAmmo), 0, stashedAmmo);
    }

    public bool CanShoot(bool onCooldown, int currentAmmo)
    {
        if (currentAmmo > 0 && !onCooldown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 FindClosestEnemyPosition(Transform playerTransform, GameObject[] enemies, float range)
    {
        Transform closestTarget = default;
        float closestSqr = Mathf.Infinity;
        Vector3 currentPosition = playerTransform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float sqrTarget = directionToTarget.sqrMagnitude;
            if (sqrTarget < closestSqr && range >= sqrTarget)
            {
                closestSqr = sqrTarget;
                closestTarget = potentialTarget.transform;
            }
        }

        if (closestTarget != default)
        {
            return closestTarget.position;
        }
        return default;
    }

    public void RotateTowardsClosestEnemy(Transform weaponTransform, Vector2 targetPosition, float weaponRotationSpeed)
    {
        float angle = Mathf.Atan2(targetPosition.y - weaponTransform.position.y, targetPosition.x - weaponTransform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        weaponTransform.rotation = Quaternion.RotateTowards(weaponTransform.rotation, targetRotation, weaponRotationSpeed * Time.deltaTime);
    }
}
