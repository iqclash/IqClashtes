using System;
using UnityEngine;

public class GameController
{
    private const int CountLooseBoolsMax = 3;
    
    public static event Action<Ball> BallChanged;

    private readonly Platform[] Platforms = new Platform[2];
    private Wall[] _walls = new Wall[2];
    private Ball _ball;

    private static Camera _camera;
    private static float _verticalSize;
    private static float _horizontalSize;
    private static int _currentCountLooseBools;
    
    private static Transform _platformContainer;
    private static Transform _ballContainer;
    private static Transform _wallsContainer;

    private static MovementController _movementController;
    private static GameEndedController _gameEndedController;
    public void Init()
    {
        _camera = Camera.main;

        _verticalSize = _camera.orthographicSize;
        _horizontalSize = _verticalSize * Screen.width / Screen.height / 2f;

        _platformContainer = ApplicationObserver.PlatformsContainer;
        _ballContainer = ApplicationObserver.BallsContainer;
        _wallsContainer = ApplicationObserver.WallsContainer;
    }

    public void StartGame()
    {
        for (var i = 0; i < Platforms.Length; i++)
            Platforms[i] = SpawnPlatform();

        for (var i = 0; i < _walls.Length; i++)
            _walls[i] = SpawnWall();

        CalculateStartPositions();

        _ball = SpawnBall();
        
        _movementController = new MovementController(Platforms, _camera, _horizontalSize);
        _gameEndedController = new GameEndedController(_ball, _verticalSize);
        _gameEndedController.BallRealesed += RespawnBall;
    }

    public void RespawnBall()
    {
        if (_currentCountLooseBools == CountLooseBoolsMax)
        {
            PageManager.Open<MainMenuPage>();
            return;
        }

        PoolObjects.Release(_ball);
        _ball = SpawnBall();
        BallChanged?.Invoke(_ball);
        _currentCountLooseBools++;
    }

    public void EndGame()
    {
        _movementController.GameEnded();
        _movementController = null;
        
        _gameEndedController.GameEnded();
        _gameEndedController = null;
        
        for (var i = 0; i < Platforms.Length; i++)
        {
            PoolObjects.Release(Platforms[i]);
            Platforms[i] = null;
        }

        for (var i = 0; i < _walls.Length; i++)
        {
            PoolObjects.Release(_walls[i]);
            _walls[i] = null;
        }

        PoolObjects.Release(_ball);
    }

    private void CalculateStartPositions()
    {
        var leftPlatform = Platforms[0];
        leftPlatform.transform.position = new Vector3(0f, _verticalSize - leftPlatform.Size.y / 2f);

        var rightPlatform = Platforms[1];
        rightPlatform.transform.position = new Vector3(0f, -_verticalSize + leftPlatform.Size.y / 2f);

        // leftWall
        InitWall(_walls[0],  new Vector3(-_horizontalSize * 2f - 0.5f, 0f),
            new Vector2(1f, _verticalSize * 2f));
        
        //rightWall
        InitWall(_walls[1],  new Vector3(+_horizontalSize * 2f + 0.5f, 0f),
            new Vector2(1f, _verticalSize * 2f));
    }

    private static void InitWall( Wall wall, Vector3 position, Vector2 colliderSize)
    {
        wall.Transform.position = position;
        wall.BoxCollider.size = colliderSize;
    }

    private static Ball SpawnBall()
    {
        var ball = PoolObjects.Get<Ball>();
        ball.Transform.SetParent(_ballContainer);
        ball.transform.position = Vector3.zero;
        return ball;
    }

    private static Platform SpawnPlatform()
    {
        var platform = PoolObjects.Get<Platform>();
        platform.Transform.SetParent(_platformContainer);
        return platform;
    }

    private static Wall SpawnWall()
    {
        var wall = PoolObjects.Get<Wall>();
        wall.Transform.SetParent(_wallsContainer);
        return wall;
    }
}