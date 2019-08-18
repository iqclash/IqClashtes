
using UnityEngine;

public class Wall : MonoBehaviour, IPoolable
{
    [SerializeField] private BoxCollider2D _boxCollider2D;
    
    private Transform _transform;

    public BoxCollider2D BoxCollider => _boxCollider2D;

    public Transform Transform => _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void OnReturnToPool()
    {
    }

    public void OnGetInPool()
    {
    }
}