
using UnityEngine;

public class ClientMovementController : MovementController
{

    public static Input _input;
    
    public ClientMovementController(Platform[] platforms, Camera camera, float horizontalSize) : base(platforms, camera, horizontalSize)
    {
    }

    public override void GameEnded()
    {
        base.GameEnded();
        _input = null;
    }

    protected override void MonoBehaviorUpdate(float timeDeltaTime)
    {
        if (_input == null)
            return;

        var transformPosition = _input.transform.position;
        MovePlatforms(ref transformPosition, timeDeltaTime);
    }
}
  
