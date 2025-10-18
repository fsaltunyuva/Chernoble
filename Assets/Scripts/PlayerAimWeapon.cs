using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private float aimSmoothSpeed = 10f; // hassasiyeti ayarlamak için

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;     
    [SerializeField] private float bulletSpeed = 10f; 

    
    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }
    
    void Update()
    {
        HandleAiming();
        HandelShooting();
    }

    void HandleAiming()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float targetAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(targetAngle, -18f, 18f);

        float currentAngle = aimTransform.eulerAngles.z;
        if (currentAngle > 180f) currentAngle -= 360f;
        float smoothedAngle = Mathf.Lerp(currentAngle, clampedAngle, Time.deltaTime * aimSmoothSpeed);

        Vector3 pivot = aimTransform.parent != null ? aimTransform.parent.position : transform.position;
        float delta = Mathf.DeltaAngle(currentAngle, smoothedAngle);
        aimTransform.RotateAround(pivot, Vector3.forward, delta);
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
