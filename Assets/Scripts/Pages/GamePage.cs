
public class GamePage : BasePage
{
    private GameController _gameController;
    
    public override void Open()
    {
        _gameController = new GameController();
        _gameController.Init();
        _gameController.StartGame();
    }

    public override void Close()
    {
        _gameController.EndGame();
        _gameController = null;
    }
}
