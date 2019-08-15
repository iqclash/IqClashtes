
using Photon.Pun;

public class GamePage : BasePage
{
    private BaseGameController _gameController;
    
    public override void Open()
    {
        if (PhotonNetwork.IsConnected)
        {
        }
        else
        {
            _gameController = new SoloGameController();
            _gameController.StartGame();   
        }
    }

    public override void Close()
    {
        _gameController.EndGame();
        _gameController = null;
    }
}
