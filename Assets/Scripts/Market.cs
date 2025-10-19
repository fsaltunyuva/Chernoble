using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    [SerializeField] private int increasedAmmoAmount = 40;
    [SerializeField] private int increasedHealthAmount = 150;
    [SerializeField] private SpriteRenderer gunSpriteRenderer;
    [SerializeField] private Player playerScript;
    [SerializeField] private PlayerAimWeapon playerAimWeaponScript;
    [SerializeField] private ZoneDamage _zoneDamageScript;
    
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
