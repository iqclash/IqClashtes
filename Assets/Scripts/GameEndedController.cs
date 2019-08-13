
using System;

public class GameEndedController
{
    public event Action BallRealesed;
    
    private readonly float VerticalSize;
    
    private GameController _gameController;
    private Ball _ball;
    private float _ballSize;
    
    public GameEndedController(Ball ball, float verticalSize)
    {
        VerticalSize = verticalSize;
        BallChanged(ball);
        
        GameController.BallChanged += BallChanged;
        ApplicationObserver.MonoBehaviorFixedUpdate += MonoBehaviorFixedUpdate;
    }

    public void GameEnded()
    {
        GameController.BallChanged -= BallChanged;
        ApplicationObserver.MonoBehaviorFixedUpdate -= MonoBehaviorFixedUpdate;
        _ball = null;
    }

    private void MonoBehaviorFixedUpdate()
    {
        if (_ball == null)
            return;
        
        var yPosition = _ball.Transform.position.y;
        if (yPosition < -VerticalSize - _ballSize || yPosition > VerticalSize + _ballSize)
        {
            _ball = null;
            BallRealesed?.Invoke();
        }
    }

    private void BallChanged(Ball ball)
    {
        _ball = ball;
        
        if (_ball == null)
            return;
        
        _ballSize = _ball.SpriteRenderer.bounds.size.x;
    }
}
