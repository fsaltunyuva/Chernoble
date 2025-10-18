using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
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
                        break;
                    case ItemType.Gun:
                        break;
                    case ItemType.Health:
                        break; 
                    case ItemType.Light:
                        break;
                    case ItemType.Suit:
                        break;
                    case ItemType.FlareGun:
                        break;
                }
                Singleton.Instance.SpendCurrency(selectedItem.cost);
            }
        }
    }
}
