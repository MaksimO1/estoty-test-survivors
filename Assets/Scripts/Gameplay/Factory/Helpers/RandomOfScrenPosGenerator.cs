using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class RandomOfScrenPosGenerator
{
    private Camera _camera;
    private Bounds _bounds;

    public RandomOfScrenPosGenerator(Camera camera, Bounds bounds)
    {
        _camera = camera;
        _bounds = bounds;
    }

    public UnityEngine.Vector3 FindSpawnLocationOutsideScreen()
    {
        Bounds cameraBounds = FindCameraBounds(_camera);
        UnityEngine.Vector3 position = GetRandomPositionWithinBounds(_bounds, cameraBounds);
        return position;
    }

    private Bounds FindCameraBounds(Camera camera)
    {
        UnityEngine.Vector3 minViewport = camera.ViewportToWorldPoint(new UnityEngine.Vector3(0, 0, camera.nearClipPlane));
        UnityEngine.Vector3 maxViewport = camera.ViewportToWorldPoint(new UnityEngine.Vector3(1, 1, camera.nearClipPlane));

        Bounds cameraBounds = new Bounds();
        cameraBounds.SetMinMax(minViewport, maxViewport);
        return cameraBounds;
    }

    private UnityEngine.Vector3 GetRandomPositionWithinBounds(Bounds bounds, Bounds cameraBounds)
    {
        UnityEngine.Vector3 randomPosition;
        do
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            randomPosition = new UnityEngine.Vector3(x, y, 0);
        }
        while (cameraBounds.Contains(randomPosition));

        return randomPosition;
    }
}
