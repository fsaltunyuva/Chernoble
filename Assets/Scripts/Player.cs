using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Animator _animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed;
    
    // Can be used for more maps
    // [SerializeField] private Transform rightSpawnPoint;
    // [SerializeField] private Transform leftSpawnPoint;
    // [SerializeField] private Transform topSpawnPoint;
    // [SerializeField] private Transform bottomSpawnPoint;

    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject arm;
    
    public bool isPlayerInGunMode = false;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Singleton.Instance.gunAmmo = Singleton.Instance.maxAmmo;
        Singleton.Instance.UpdateAmmoText();

        // switch (Singleton.Instance.spawnPoint)
        // {
        //     case SpawnPoint.Right:
        //         transform.position = rightSpawnPoint.position;
        //         break;
        //     case SpawnPoint.Left:
        //         transform.position = leftSpawnPoint.position;
        //         break;
        //     case SpawnPoint.Top:
        //         transform.position = topSpawnPoint.position;
        //         break;
        //     case SpawnPoint.Bottom:
        //         transform.position = bottomSpawnPoint.position;
        //         break;
        // }
    }
    
    void Update()
    {
        if (!Singleton.Instance.isMovementEnabled)
        {
            _movement = Vector2.zero;
        }
        else
        {
            _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        
        if (Input.GetMouseButton(1)) // Holding right mouse button
        {
            Singleton.Instance.isMovementEnabled = false;
            _animator.SetBool("gun", true);
            gun.SetActive(true);
            isPlayerInGunMode = true;
        }
        
        if (Input.GetMouseButtonUp(1)) // Releasing right mouse button
        {
            Singleton.Instance.isMovementEnabled = true;
            _animator.SetBool("gun", false);
            gun.SetActive(false);
            isPlayerInGunMode = false;
        }

        FlipSprite(_movement.x);
        
        if (_movement == Vector2.zero)
        {
            _animator.SetBool("run", false);
        }
        else
        {
            _animator.SetBool("run", true);
        }
    }
    
    // private void FlipSprite()
    // {
    //     if (_movement.x > 0)
    //         transform.localScale = new Vector3(1, 1, 1);
    //     else if (_movement.x < 0)
    //         transform.localScale = new Vector3(-1, 1, 1);
    // }

    void FlipSprite(float moveX)
    {
        if (moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        _rb.velocity = _movement * speed * Time.fixedDeltaTime;
    }
}
