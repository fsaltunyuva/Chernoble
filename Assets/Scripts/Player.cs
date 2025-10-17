using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;
    [SerializeField] private float speed;
    [SerializeField] public int currency;
    [SerializeField] TextMeshProUGUI currencyText;
    
    //TODO: Interact with objects in the game world
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        _rb.velocity = _movement * speed * Time.fixedDeltaTime;
    }
    
    public void AddCurrency(int amount)
    {
        currency += amount;
        currencyText.text = currency.ToString();
    }
    
    public bool SpendCurrency(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            currencyText.text = currency.ToString();
            return true;
        }
        return false;
    }
}
