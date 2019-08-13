using System;

using UnityEngine;



public class ApplicationObserver : MonoBehaviour
{
    [SerializeField] private Transform _platrofmContainer;
    [SerializeField] private Transform _ballContainer;
    [SerializeField] private Transform _wallsContainer;
    
    public static event Action<float> MonoBehaviorUpdate;
    public static event Action MonoBehaviorFixedUpdate;
    
    public static Transform PlatformsContainer => _instance._platrofmContainer;
    
    public static Transform BallsContainer => _instance._ballContainer;
    
    public static Transform WallsContainer => _instance._wallsContainer;

    private static ApplicationObserver _instance;

    private void Awake()
    {
        _instance = this;

        PoolObjects.Instance.Init();
    }

    public void Update()
    {
        MonoBehaviorUpdate?.Invoke(Time.deltaTime);
    }

    public void FixedUpdate()
    {
        MonoBehaviorFixedUpdate?.Invoke();
    }
}