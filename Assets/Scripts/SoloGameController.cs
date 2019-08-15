
using UnityEngine;

public class SoloGameController : BaseGameController
{
    public override void StartGame()
    {
        for (var i = 0; i < Platforms.Length; i++)
            Platforms[i] = Spawner.SpawnPlatform(ref _platformContainer);

        for (var i = 0; i < _walls.Length; i++)
            _walls[i] = Spawner.SpawnWall(ref _wallsContainer);

        CalculateStartPositions();

        _ball = Spawner.SpawnBall(ref _ballContainer);
        
        _movementController = new MovementController(Platforms, _camera, _horizontalSize);
        _gameEndedController = new GameEndedController(_ball, _verticalSize);
        _gameEndedController.BallRealesed += RespawnBall;
    }

    public override void EndGame()
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
    
    public override void RespawnBall()
    {
        if (_currentCountLooseBools == CountLooseBoolsMax)
        {
            PageManager.Open<MainMenuPage>();
            return;
        }

        PoolObjects.Release(_ball);
        
        _ball = Spawner.SpawnBall(ref _ballContainer);
        
        _currentCountLooseBools++;
        base.RespawnBall();
    }
    
    protected override void CalculateStartPositions()
    {
        var leftPlatform = Platforms[0];
        leftPlatform.transform.position = new Vector3(0f, _verticalSize - leftPlatform.Size.y / 2f);

        var rightPlatform = Platforms[1];
        rightPlatform.transform.position = new Vector3(0f, -_verticalSize + leftPlatform.Size.y / 2f);

        // leftWall
        SetWallPosition(_walls[0],  new Vector3(-_horizontalSize * 2f - 0.5f, 0f),
            new Vector2(1f, _verticalSize * 2f));
        
        //rightWall
        SetWallPosition(_walls[1],  new Vector3(+_horizontalSize * 2f + 0.5f, 0f),
            new Vector2(1f, _verticalSize * 2f));
    }
}