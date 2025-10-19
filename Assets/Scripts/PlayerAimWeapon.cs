using System;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private float aimSmoothSpeed = 10f; // hassasiyeti ayarlamak için

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject flareBulletPrefab;
    [SerializeField] private Transform firePoint;     
    [SerializeField] private float bulletSpeed = 10f; 
    
    [SerializeField] private SpriteRenderer spriteRenderer; // Player'ın SpriteRenderer'ı
    
    [SerializeField] private SpriteRenderer gunSpriteRenderer; // Silahın SpriteRenderer'ı
    private Player _playerScript;

    [SerializeField] private float flareDistanceDelay = 1.25f;

    private void Start()
    {
        _playerScript = GetComponent<Player>();
    }

    void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    void HandleAiming()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Karakterden mouse'a doğru yön
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        // Mouse'un yönüne göre z açısını hesapla
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Silahın aimTransform'unu direkt o yöne döndür
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        
        // --- Sağ/Sol flip kontrolü ---
        bool facingLeft = angle > 90 || angle < -90;
        spriteRenderer.flipX = facingLeft;

        // --- Silahın Y scale'ini çevirerek ters çevirme (rotation bozmaz) ---
        Vector3 scale = aimTransform.localScale;
        scale.y = facingLeft ? -1 : 1;
        aimTransform.localScale = scale;

        // --- FirePoint yön düzeltmesi ---
        Vector3 fireScale = firePoint.localScale;
        fireScale.x = facingLeft ? -Mathf.Abs(fireScale.x) : Mathf.Abs(fireScale.x);
        firePoint.localScale = fireScale;
    }
    
    void HandleShooting()
    {
        if (Singleton.Instance.IsThereEnoughAmmo(1) && Input.GetMouseButtonDown(0) && _playerScript.isPlayerInGunMode)
        {
            // TODO: Play click sfx when there is no ammo left (use spend ammo's bool return value)
            if(Singleton.Instance.currentWeapon == WeaponType.Glock)
            {
                animator.CrossFadeInFixedTime("Gun Shot Animation", 0f);
                ShootBullet();
            }
            else if(Singleton.Instance.currentWeapon == WeaponType.FlareGun)
            {
                ShootFlare();
            }
        }
    }
    
    void ShootBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.right * bulletSpeed;
            Singleton.Instance.SpendAmmo(1);
        }
    }
    
    void ShootFlare()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(flareBulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.right * bulletSpeed;
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.StartCoroutine(bulletScript.SetFlareVelocityToZeroAfterDelay(0.5f));
            Singleton.Instance.SpendAmmo(1, true);
        }
    }
}
