using System;
using UnityEngine;
using UnityEngine.UI;

public class ZoneDamage : MonoBehaviour
{
    [SerializeField] private int healthDecreaseMultiplier1;
    [SerializeField] private int healthDecreaseMultiplier2;
    [SerializeField] private int healthDecreaseMultiplier3;
    [SerializeField] private int healthDecreaseMultiplier4;
    [SerializeField] private int enemyDamageMultiplier;
    private float health = 100f;
    [SerializeField] Slider slider;


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("greenZone"))
        {
            health -= healthDecreaseMultiplier1 * Time.deltaTime;
            Debug.Log("health: " + health);
            if (health < 0) health = 0;
            slider.value = health;
        }
        else if (other.CompareTag("blueZone"))
        {
            health -= healthDecreaseMultiplier2 * Time.deltaTime;
            Debug.Log("health: " + health);
            if (health < 0) health = 0;
            slider.value = health;
        }
        else if (other.CompareTag("redZone"))
        {
            health -= healthDecreaseMultiplier3 * Time.deltaTime;
            Debug.Log("health: " + health);
            if (health < 0) health = 0;
            slider.value = health;
        }
        else if (other.CompareTag("purpleZone"))
        {
            health -= healthDecreaseMultiplier4 * Time.deltaTime;
            Debug.Log("health: " + health);
            if (health < 0) health = 0;
            slider.value = health;
        }
        
        if(other.CompareTag("enemy"))
        {
            health -= enemyDamageMultiplier * Time.deltaTime;
            if (health < 0) health = 0;
            slider.value = health;
        }
    }

    public void AddHealth(int healthAmount)
    {
        if (health + healthAmount >= 0) health += healthAmount;
        else if (health > 100) health = 100;

        slider.value = health;
        Debug.Log("current health: " + health);
    }
    
    public void SetHealth(int healthAmount)
    {
        health = 100;
        slider.value = health;
    }

}
