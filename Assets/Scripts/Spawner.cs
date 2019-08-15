
using UnityEngine;

public static class Spawner
{
    public static Ball SpawnBall(ref Transform parent)
    {
        var ball = PoolObjects.Get<Ball>();
        ball.Transform.SetParent(parent);
        ball.transform.position = Vector3.zero;
        return ball;
    }

    public static Platform SpawnPlatform(ref Transform parent)
    {
        var platform = PoolObjects.Get<Platform>();
        platform.Transform.SetParent(parent);
        return platform;
    }

    public static Wall SpawnWall(ref Transform parent)
    {
        var wall = PoolObjects.Get<Wall>();
        wall.Transform.SetParent(parent);
        return wall;
    }
}
