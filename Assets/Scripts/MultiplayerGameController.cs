
using Photon.Pun;

public class MultiplayerGameController : SoloGameController
{
    public override void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            base.StartGame();
        }
        else
        {
            
        }
    }

    public override void EndGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            base.EndGame();
        }
        else
        {
            
        }
    }

    protected override void CalculateStartPositions()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            base.CalculateStartPositions();
        }
        else
        {
            
        }
    }
}
