
using Photon.Pun;
using UnityEngine;

public class Input : MonoBehaviourPun
{
    private Camera _camera;
    
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ClientMovementController._input = this;
        }
        else
        {
            _camera = Camera.main;
            ApplicationObserver.MonoBehaviorUpdate += MonoBehaviorUpdate;
        }
    }

    private void MonoBehaviorUpdate(float deltaTime)
    {
        transform.position = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
    }
    
    private void OnDestroy()
    {
        ApplicationObserver.MonoBehaviorUpdate -= MonoBehaviorUpdate;
    }
}
