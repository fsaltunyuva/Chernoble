using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int coinValue;

    public int GetCoinValue()
    {
        return coinValue;
    }
}
