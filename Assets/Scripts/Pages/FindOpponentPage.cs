
using Photon.Pun;
using Photon.Realtime;

public class FindOpponentPage : BasePage
{
    public override void Open()
    {
    }

    public override void OnConnected()
    {
        MultiplayerController.JoinOrCreateRoom();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        MultiplayerController.Disconnect();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            PageManager.Open<GamePage>();
    }

    public override void Close()
    {
    }
}
