using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private float aimSmoothSpeed = 10f; // hassasiyeti ayarlamak için

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;     
    [SerializeField] private float bulletSpeed = 10f; 
    
    [SerializeField] private SpriteRenderer spriteRenderer; // Player'ın SpriteRenderer'ı
    
    [SerializeField] private SpriteRenderer gunSpriteRenderer; // Silahın SpriteRenderer'ı
    
    void Update()
    {
        HandleAiming();
        HandelShooting();
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
        
        // if (angle > 90 || angle < -90)
        // {
        //     spriteRenderer.flipX = true;   // karakter sola bakıyor
        //     // rotate aim transform 180 degrees on x axis
        //     aimTransform.localRotation = Quaternion.Euler(180, 0, angle);
        //     // gunSpriteRenderer.flipY = true;
        // }
        // else
        // {
        //     spriteRenderer.flipX = false;  // karakter sağa bakıyor
        //     aimTransform.localRotation = Quaternion.Euler(0, 0, angle);
        //     // gunSpriteRenderer.flipY = false;
        // }
        //
        // Vector3 localScale = firePoint.localScale;
        // localScale.x = (angle > 90 || angle < -90) ? -Mathf.Abs(localScale.x) : Mathf.Abs(localScale.x);
        // firePoint.localScale = localScale;
        
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
        
        
        // if (angle > 90 || angle < -90)
        // {
        //     aimTransform.localScale = new Vector3(1, -1, 1);
        // }
        // else
        // {
        //     aimTransform.localScale = new Vector3(1, 1, 1);
        // }
    }
    
    void HandelShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //animator.SetTrigger("shot");
            animator.CrossFadeInFixedTime("Gun Shot Animation", 0f);
            ShootBullet();
        }
    }
    
    void ShootBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = firePoint.right * bulletSpeed;
        }
    }
}
