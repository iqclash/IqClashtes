
using Photon.Pun;

public class GamePage : BasePage
{
    private BaseGameController _gameController;
    
    public override void Open()
    {
        _gameController = PhotonNetwork.IsConnected
            ? (BaseGameController) new MultiplayerGameController()
            : new SoloGameController();

        _gameController.StartGame();
    }

    public override void Close()
    {
        _gameController.EndGame();
        _gameController = null;
    }
}
