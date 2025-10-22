using System;
using TMPro;
using UnityEngine;

public enum SpawnPoint
{
    Top,
    Bottom,
    Left,
    Right
}

public enum WeaponType
{
    Glock,
    FlareGun
}
public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; } // This is the instance of the Singleton class.
    
    [SerializeField] public int currency;
    [SerializeField] public int gunAmmo;
    [SerializeField] public int flareAmmo;
    [SerializeField] public int maxAmmo;
    [SerializeField] TextMeshProUGUI currencyText;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI flareAmmoText;
    // public SpawnPoint spawnPoint = SpawnPoint.Top;
    public bool isMovementEnabled = true;
    public int previoslyLoadedSceneIndex = -1;
    public bool isMarketCanvasOn = false;
    public WeaponType currentWeapon = WeaponType.Glock;
    public int playerDamage = 10;
    
    public bool isPlayerBoughtFlareGun = false;

    private void Awake() 
    {
        // TODO: Uncomment this if you add new scenes to the game.
        // if (Instance != null && Instance != this) // If there is an instance, and it's not me, delete myself. 
        // { 
        //     Destroy(gameObject); 
        // } 
        // else // Otherwise, set the instance to me and don't destroy me.
        // { 
        //     Instance = this; 
        //     DontDestroyOnLoad(gameObject); // This will keep the Singleton object alive between scenes.
        // } 
        Instance = this;
    }

    private void Start()
    {
        currencyText.text = currency.ToString();
        // ammoText.text = gunAmmo.ToString();
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
    
    public bool AddAmmo(int amount)
    {
        if (gunAmmo + amount <= maxAmmo)
        {
            gunAmmo += amount;
            ammoText.text = gunAmmo.ToString();
            return true;
        }
        return false;
    }
    
    public bool SpendAmmo(int amount, bool isFlareAmmo = false)
    {
        if (isFlareAmmo)
        {
            if (flareAmmo >= amount)
            {
                flareAmmo -= amount;
                flareAmmoText.text = flareAmmo.ToString();
                return true;
            }
            return false;
        }
        
        // Normal gun ammo spending
        if (gunAmmo >= amount)
        {
            gunAmmo -= amount;
            ammoText.text = gunAmmo.ToString();
            return true;
        }
        return false;
    }
    
    public void UpdateAmmoText()
    {
        ammoText.text = gunAmmo.ToString();
    }

    public bool IsThereEnoughAmmo(int amount)
    {
        return gunAmmo >= amount;
    }
    
    public bool IsThereEnoughFlareAmmo(int amount)
    {
        return flareAmmo >= amount;
    }
}