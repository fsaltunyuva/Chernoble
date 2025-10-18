using TMPro;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; } // This is the instance of the Singleton class.
    
    [SerializeField] public int currency;
    [SerializeField] TextMeshProUGUI currencyText;

    private void Awake() 
    {
        if (Instance != null && Instance != this) // If there is an instance, and it's not me, delete myself. 
        { 
            Destroy(gameObject); 
        } 
        else // Otherwise, set the instance to me and don't destroy me.
        { 
            Instance = this; 
            DontDestroyOnLoad(gameObject); // This will keep the Singleton object alive between scenes.
        } 
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