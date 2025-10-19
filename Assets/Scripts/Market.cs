using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    [SerializeField] private int increasedAmmoAmount = 40;
    [SerializeField] private int increasedHealthAmount = 150;
    [SerializeField] private int increasedLightRadius = 5;
    [SerializeField] private int increasedLightIntensity = 3;
    [SerializeField] private SpriteRenderer gunSpriteRenderer;
    [SerializeField] private Player playerScript;
    [SerializeField] private PlayerAimWeapon playerAimWeaponScript;
    [SerializeField] private ZoneDamage _zoneDamageScript;
    
    [SerializeField] private Light2D playerLight;
     
    public void TryToBuyItem()
    {
        GameObject selectedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        
        if (selectedButton != null)
        {
            Item selectedItem = selectedButton.GetComponent<Item>();
            // TODO: Play buy sound effect
            
            if (Singleton.Instance.currency >= selectedItem.cost)
            {
                switch (selectedItem.type)
                {
                    case ItemType.Ammo:
                        Singleton.Instance.maxAmmo = increasedAmmoAmount;
                        Singleton.Instance.gunAmmo = Singleton.Instance.maxAmmo;
                        Singleton.Instance.UpdateAmmoText();
                        break;
                    case ItemType.Gun:
                        Singleton.Instance.playerDamage = 20;
                        playerScript.UpgradeToAK47();
                        playerAimWeaponScript.UpgradeToAK47();
                        break;
                    case ItemType.Health:
                        _zoneDamageScript.SetMaxHealth(increasedHealthAmount);
                        break; 
                    case ItemType.Light:
                        playerLight.pointLightOuterRadius = increasedLightRadius;
                        playerLight.intensity = increasedLightIntensity;
                        break;
                    case ItemType.Suit:
                        break;
                    case ItemType.FlareGun:
                        break;
                }
                Singleton.Instance.SpendCurrency(selectedItem.cost);
                selectedButton.GetComponent<Button>().interactable = false;
            }
        }
    }
}
