using Photon.Pun;
using UnityEngine;

public class MultiplayerGameController : BaseGameController
{
    private ClientMovementController _clientMovementController;
    private Input _inputClient;
    
    public override void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
            StartGameServer();
        else
            StartGameClient();
    }

    private void StartGameServer()
    {
        for (var i = 0; i < Platforms.Length; i++)
            Platforms[i] = Spawner.SpawnPlatformMultiplayer(ref _platformContainer);

        for (var i = 0; i < _walls.Length; i++)
            _walls[i] = Spawner.SpawnWall(ref _wallsContainer);

        CalculateStartPositions();

        _ball = Spawner.SpawnBallMultiplayer(ref _ballContainer);

        _movementController = new MovementController(new Platform[1]{Platforms[0]}, _camera, _horizontalSize);
        _clientMovementController = new ClientMovementController(new Platform[1]{Platforms[1]}, _camera, _horizontalSize );
        
        _gameEndedController = new GameEndedController(_ball, _verticalSize);
        _gameEndedController.BallRealesed += RespawnBall;
    }

    private void StartGameClient()
    {
        _inputClient = Spawner.SpawnInput(ref _platformContainer);
    }

    public override void EndGame()
    {
        if (PhotonNetwork.IsMasterClient)
            EndGameServer();
        else
            EndGameClient();
    }

    private void EndGameServer()
    {
        _movementController?.GameEnded();
        _clientMovementController?.GameEnded();
        _gameEndedController?.GameEnded();
        
        _movementController = null;
        _clientMovementController = null;
        _gameEndedController = null;

        for (var i = 0; i < _walls.Length; i++)
        {
            if(_walls[i] == null)
                continue;
            
            PoolObjects.Release(_walls[i]);
            _walls[i] = null;
        }
        
    }

    private void EndGameClient()
    {
        MultiplayerController.Disconnect();
    }

    public override void RespawnBall()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        if (_currentCountLooseBools == CountLooseBoolsMax)
        {
            MultiplayerController.Disconnect();
            return;
        }
        
        _ball.Transform.position = Vector3.zero;
        _ball.RandomBallSettings();
        
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