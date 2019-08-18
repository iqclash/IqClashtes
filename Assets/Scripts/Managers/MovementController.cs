using UnityEngine;

public class MovementController
{
    private const float PlatformSpeed = 10f;

    private Platform[] _movementPlatforms;

    private Camera _camera;
    private static float _horizontalSize;

    public MovementController(Platform[] platforms, Camera camera, float horizontalSize)
    {
        _movementPlatforms = platforms;
        _camera = camera;
        _horizontalSize = horizontalSize * 2f;

        ApplicationObserver.MonoBehaviorUpdate += MonoBehaviorUpdate;
    }

    public virtual void GameEnded()
    {
        ApplicationObserver.MonoBehaviorUpdate -= MonoBehaviorUpdate;
        _movementPlatforms = null;
        _camera = null;
    }

    protected virtual void MonoBehaviorUpdate(float timeDeltaTime)
    {
        if (_movementPlatforms.Length == 0)
            return;

        var mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

        MovePlatforms(ref mousePosition, timeDeltaTime);
    }

    protected void MovePlatforms(ref Vector3 mousePosition,float timeDeltaTime)
    {
        for (var i = 0; i < _movementPlatforms.Length; i++)
        {
            var platform = _movementPlatforms[i];
            var position = platform.Transform.position;

            if (Mathf.Approximately(position.x, mousePosition.x))
                continue;

            position.x += timeDeltaTime * PlatformSpeed * (mousePosition.x - position.x);
            position.x = Mathf.Clamp(position.x, (-_horizontalSize + platform.Size.x / 2f),
                _horizontalSize - platform.Size.x / 2f);

            platform.Transform.position = position;
        }
    }
}