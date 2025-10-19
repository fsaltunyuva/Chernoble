using DG.Tweening;
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
    
    [SerializeField] private AudioClip purchaseSoundEffect;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject flareGunTutorialText;
    
    public void TryToBuyItem()
    {
        GameObject selectedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        
        if (selectedButton != null)
        {
            Item selectedItem = selectedButton.GetComponent<Item>();
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
                        _zoneDamageScript.healthDecreaseMultiplier1 -= 3;
                        _zoneDamageScript.healthDecreaseMultiplier2 -= 3;
                        _zoneDamageScript.healthDecreaseMultiplier3 -= 3;
                        _zoneDamageScript.healthDecreaseMultiplier4 -= 3;
                        _zoneDamageScript.enemyDamageMultiplier -= 5;
                        break;
                    case ItemType.FlareGun:
                        Singleton.Instance.isPlayerBoughtFlareGun = true;
                        flareGunTutorialText.SetActive(true);
                        // shake tutorial text using DOTween and loop
                        flareGunTutorialText.GetComponent<RectTransform>().DOShakePosition(1f, new Vector3(0.1f, 0.1f, 0f), 2, 90, false, true).SetLoops(-1, LoopType.Restart);
                        break;
                }
                Singleton.Instance.SpendCurrency(selectedItem.cost);
                selectedButton.GetComponent<Button>().interactable = false;
                // Play one shot sound effect for successful purchase
                AudioSource.PlayClipAtPoint(purchaseSoundEffect, Camera.main.transform.position);
            }
        }
    }
}
