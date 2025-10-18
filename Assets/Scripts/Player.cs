using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;
    [SerializeField] private float speed;
    
    [SerializeField] private Transform rightSpawnPoint;
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform topSpawnPoint;
    [SerializeField] private Transform bottomSpawnPoint;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

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
    }

    void FixedUpdate()
    {
        _rb.velocity = _movement * speed * Time.fixedDeltaTime;
    }
}
