using System;
using UnityEngine;
using UnityEngine.UI;

public class ZoneDamage : MonoBehaviour
{
    [SerializeField] private int healthDecreaseMultiplier1;
    [SerializeField] private int healthDecreaseMultiplier2;
    [SerializeField] private int healthDecreaseMultiplier3;
    private float health = 100f;
    [SerializeField] Slider slider;


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("zone1"))
        {
            health -= healthDecreaseMultiplier1 * Time.deltaTime;
            Debug.Log("health: " + health);
            if (health < 0) health = 0;
            slider.value = health;
        }
        else if (other.CompareTag("zone2"))
        {
            health -= healthDecreaseMultiplier2 * Time.deltaTime;
            Debug.Log("health: " + health);
            if (health < 0) health = 0;
            slider.value = health;
        }
        else if (other.CompareTag("zone3"))
        {
            health -= healthDecreaseMultiplier3 * Time.deltaTime;
            Debug.Log("health: " + health);
            if (health < 0) health = 0;
            slider.value = health;
        }
    }

    public void AddHealth(int healthAmount)
    {
        if (health + healthAmount >= 0) health += healthAmount;
        slider.value = health;
        Debug.Log("current health: " + health);
    }

}
