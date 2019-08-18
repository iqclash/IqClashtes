using Photon.Pun;
using UnityEngine;

public class Ball : MonoBehaviourPun, IPunInstantiateMagicCallback, IPoolable
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CircleCollider2D _circleCollider;
    [SerializeField] private PhotonView _photonView;

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
        RandomBallSettings();
    }

    public void RandomBallSettings()
    {
        _spriteRenderer.color = Random.ColorHSV();

        var size = Random.Range(0.3f, 2f);
        _spriteRenderer.size = Vector2.one * size;
        _circleCollider.radius = size / 2f;
        
        _rigidbody2D.velocity = Vector2.zero;
        var direction = new Vector2(Random.Range(0, 2) == 1 ? -1 : 1, Random.Range(1.5f, -1.5f));
        _rigidbody2D.AddForce(direction * Random.Range(300f, 400f), ForceMode2D.Force);
        
        
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        var color = _spriteRenderer.color;
        _photonView.RPC(nameof(GetColorAndSize), RpcTarget.Others, color.r, color.g, color.b,
            _spriteRenderer.size.x);
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if(PhotonNetwork.IsMasterClient)
            RandomBallSettings();
        else
        {
            Destroy(_rigidbody2D);
            Destroy(_circleCollider);
        }
    }

    [PunRPC]
    private void GetColorAndSize(float r, float g, float b, float size)
    {
        if (PhotonNetwork.IsMasterClient)
            return;

        transform.position = Vector3.zero;
        _spriteRenderer.color = new Color(r, g, b);
        _spriteRenderer.size = new Vector2(size, size);
    }
}