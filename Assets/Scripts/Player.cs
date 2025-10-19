using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Animator _animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed;
    
    // Can be used for more maps
    // TODO: Move them to Interactable.cs
    [SerializeField] public Transform rightSpawnPoint;
    [SerializeField] public Transform leftSpawnPoint;
    [SerializeField] public Transform topSpawnPoint;
    [SerializeField] public Transform bottomSpawnPoint;
    [SerializeField] public Transform rightExitSpawnPoint;
    [SerializeField] public Transform leftExitSpawnPoint;
    [SerializeField] public Transform topExitSpawnPoint;
    [SerializeField] public Transform bottomExitSpawnPoint;

    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject arm;
    
    public bool isPlayerInGunMode = false;
    
    [SerializeField] private Sprite FlareGunSprite;
    [SerializeField] private Sprite GlockGunSprite;
    [SerializeField] private SpriteRenderer gunSpriteRenderer;
    
    
    [SerializeField] private GameObject ak47gun;
    [SerializeField] private GameObject ak47arm;
    
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
            Singleton.Instance.currentWeapon = WeaponType.Glock;
            _animator.SetBool("gun", true);
            gunSpriteRenderer.sprite = GlockGunSprite;
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
        
        if (Input.GetMouseButton(2)) // Holding middle mouse button
        {
            Singleton.Instance.isMovementEnabled = false;
            Singleton.Instance.currentWeapon = WeaponType.FlareGun;
            _animator.SetBool("gun", true);
            gunSpriteRenderer.sprite = FlareGunSprite;
            gun.SetActive(true);
            isPlayerInGunMode = true;
        }
        
        if (Input.GetMouseButtonUp(2)) // Releasing middle mouse button
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

    public void UpgradeToAK47()
    {
        gun = ak47gun;
        arm = ak47arm;
    }
}
