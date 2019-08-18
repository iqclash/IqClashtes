using Photon.Pun;
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

    public static Ball SpawnBallMultiplayer(ref Transform parent)
    {
        var ball = PhotonNetwork.InstantiateSceneObject("Ball", Vector3.zero, Quaternion.identity).GetComponent<Ball>();
        ball.Transform.SetParent(parent);
        return ball;
    }

    public static Platform SpawnPlatformMultiplayer(ref Transform parent)
    {
        var platform = PhotonNetwork.InstantiateSceneObject("Platform", Vector3.zero, Quaternion.identity)
                                    .GetComponent<Platform>();
        platform.Transform.SetParent(parent);
        return platform;
    }

    public static Wall SpawnWallMultiplayer(ref Transform parent)
    {
        var wall = PhotonNetwork.InstantiateSceneObject("Wall", Vector3.zero, Quaternion.identity).GetComponent<Wall>();
        wall.Transform.SetParent(parent);
        return wall;
    }

    public static Input SpawnInput(ref Transform parent) =>
        PhotonNetwork.Instantiate("Input", Vector3.zero, Quaternion.identity).GetComponent<Input>();
    
}