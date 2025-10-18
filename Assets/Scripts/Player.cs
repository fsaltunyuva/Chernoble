using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Animator _animator;
    [SerializeField] private float speed;
    
    [SerializeField] private Transform rightSpawnPoint;
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform topSpawnPoint;
    [SerializeField] private Transform bottomSpawnPoint;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        switch (Singleton.Instance.spawnPoint)
        {
            case SpawnPoint.Right:
                transform.position = rightSpawnPoint.position;
                break;
            case SpawnPoint.Left:
                transform.position = leftSpawnPoint.position;
                break;
            case SpawnPoint.Top:
                transform.position = topSpawnPoint.position;
                break;
            case SpawnPoint.Bottom:
                transform.position = bottomSpawnPoint.position;
                break;
        }
    }
    
    void Update()
    {
        if (!Singleton.Instance.isInputEnabled)
        {
            _movement = Vector2.zero;
            return;
        }
        _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        FlipSprite();
        
        if (_movement == Vector2.zero)
        {
            _animator.SetBool("run", false);
        }
        else
        {
            _animator.SetBool("run", true);
        }
    }
    
    private void FlipSprite()
    {
        if (_movement.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (_movement.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void FixedUpdate()
    {
        _rb.velocity = _movement * speed * Time.fixedDeltaTime;
    }
}
