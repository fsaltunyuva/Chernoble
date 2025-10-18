using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ItemType
{
    Health,
    Light,
    Gun,
    Ammo,
    Suit,
    FlareGun // ??
}

public class Item : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costText;
    public int cost;
    public ItemType type;

    private void Start()
    {
        cost = int.Parse(costText.text);
    }
}
