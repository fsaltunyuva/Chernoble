using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // 2 saniye sonra yok olsun
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.gameObject.CompareTag("camera boundary"))
            Destroy(gameObject);
    }
}