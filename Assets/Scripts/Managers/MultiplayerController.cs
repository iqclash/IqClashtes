
using Photon.Pun;
using Photon.Realtime;

public static class MultiplayerController
{
   
   public static void Connect()
   {
      if (PhotonNetwork.IsConnected)
         JoinOrCreateRoom();
      else
         PhotonNetwork.ConnectUsingSettings();
   }

   public static void JoinOrCreateRoom()
   {
      PhotonNetwork.JoinOrCreateRoom("TestRoom", RoomOptions, TypedLobby);
   }

   public static void Disconnect()
   {
      PhotonNetwork.Disconnect();
      PhotonNetwork.DestroyAll();
      
      PageManager.Open<MainMenuPage>();
   }

   private static RoomOptions RoomOptions = new RoomOptions {MaxPlayers = 2};
   
   private static TypedLobby TypedLobby = new TypedLobby {Type = LobbyType.Default};
}
