
using Photon.Pun;
using Photon.Realtime;

public class FindOpponentPage : BasePage
{
    public override void Open()
    {
        MultiplayerController.Connect();
    }

    public override void OnConnectedToMaster()
    {
        MultiplayerController.JoinOrCreateRoom();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        MultiplayerController.Disconnect();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        MultiplayerController.Disconnect();
    }

    public override void OnCreatedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            PageManager.Open<GamePage>();
    }
    
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            PageManager.Open<GamePage>();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            PageManager.Open<GamePage>();
    }

    public override void Close()
    {
    }
}
