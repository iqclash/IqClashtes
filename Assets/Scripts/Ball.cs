
using UnityEngine;

public class Ball : MonoBehaviour, IPoolable
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CircleCollider2D _circleCollider;
    
    private Transform _transform;

    public Transform Transform => _transform;

    public Rigidbody2D Rigidbody => _rigidbody2D;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    private void Awake()
    {
        _transform = transform;
    }
    
    public void OnReturnToPool()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    public void OnGetInPool()
    {
        _spriteRenderer.color = Random.ColorHSV();
        
        var size = Random.Range(0.3f, 2f);
        _spriteRenderer.size = Vector2.one * size;
        _circleCollider.radius = size / 2f;
        
        var direction = new Vector2(Random.Range(0,2) == 1 ? -1 : 1,Random.Range(1.5f, -1.5f));
        _rigidbody2D.AddForce(direction * Random.Range(300f, 400f), ForceMode2D.Force);
    }
}
