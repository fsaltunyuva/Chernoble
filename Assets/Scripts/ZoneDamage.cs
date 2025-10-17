using System;
using UnityEngine;

public class ZoneDamage : MonoBehaviour
{
    [SerializeField] private int healthDecreaseMultiplier1;
    [SerializeField] private int healthDecreaseMultiplier2;
    [SerializeField] private int healthDecreaseMultiplier3;
    private float health = 1000f;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("zone1"))
        {
            health -= healthDecreaseMultiplier1 * Time.deltaTime;
            Debug.Log("health: " + health);
        }
        else if (other.CompareTag("zone2"))
        {
            health -= healthDecreaseMultiplier2 * Time.deltaTime;
            Debug.Log("health: " + health);
        }
        else if (other.CompareTag("zone3"))
        {
            health -= healthDecreaseMultiplier3 * Time.deltaTime;
            Debug.Log("health: " + health);
        }
    }

}
