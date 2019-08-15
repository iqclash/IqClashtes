using System;
using UnityEngine;

public abstract class BaseGameController
{
    public static event Action<Ball> BallChanged;
    
    protected const int CountLooseBoolsMax = 3;

    protected readonly Platform[] Platforms = new Platform[2];
    protected Wall[] _walls = new Wall[2];
    protected Ball _ball;

    protected Camera _camera;
    protected float _verticalSize;
    protected float _horizontalSize;
    protected int _currentCountLooseBools;

    protected Transform _platformContainer;
    protected Transform _ballContainer;
    protected Transform _wallsContainer;

    protected MovementController _movementController;
    protected GameEndedController _gameEndedController;

    public BaseGameController()
    {
        _camera = Camera.main;

        _verticalSize = _camera.orthographicSize;
        _horizontalSize = _verticalSize * Screen.width / Screen.height / 2f;

        _platformContainer = ApplicationObserver.PlatformsContainer;
        _ballContainer = ApplicationObserver.BallsContainer;
        _wallsContainer = ApplicationObserver.WallsContainer;
    }

    public abstract void StartGame();
    public abstract void EndGame();

    public virtual void RespawnBall()
    {
        BallChanged?.Invoke(_ball);
    }

    protected abstract void CalculateStartPositions();

    protected void SetWallPosition(Wall wall, Vector3 position, Vector2 colliderSize)
    {
        wall.Transform.position = position;
        wall.BoxCollider.size = colliderSize;
    }
}