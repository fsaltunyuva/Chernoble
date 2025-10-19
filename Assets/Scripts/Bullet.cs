using System;
using System.Collections;
using UnityEngine;

public enum BulletType
{
    GlockBullet,
    FlareBullet
}

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f;
    public BulletType bulletType = BulletType.GlockBullet;

    void Start()
    {
        if(bulletType != BulletType.FlareBullet)
            Destroy(gameObject, lifeTime); // 2 saniye sonra yok olsun
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("camera boundary"))
        {
            if (bulletType == BulletType.GlockBullet)
                Destroy(gameObject);
        }
    }
    
    public IEnumerator SetFlareVelocityToZeroAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}