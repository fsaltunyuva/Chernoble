using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // 2 saniye sonra yok olsun
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Burada düşmana çarpınca bir şey yapmak istersen ekleyebilirsin
        
        //Destroy(gameObject);
    }
}