using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ZoneDamage : MonoBehaviour
{
    [SerializeField] private int healthDecreaseMultiplier1;
    [SerializeField] private int healthDecreaseMultiplier2;
    [SerializeField] private int healthDecreaseMultiplier3;
    [SerializeField] private int healthDecreaseMultiplier4;
    [SerializeField] private int enemyDamageMultiplier;
    private float health = 100f;
    [SerializeField] Slider slider;

    [SerializeField] Image bloodyScreen;
    [SerializeField] private float fadeDuration = 1f;

    [Header("Player Feedback")]
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Transform playerTransform;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("greenZone"))
            ApplyZoneDamage(healthDecreaseMultiplier1);
        else if (other.CompareTag("blueZone"))
            ApplyZoneDamage(healthDecreaseMultiplier2);
        else if (other.CompareTag("redZone"))
            ApplyZoneDamage(healthDecreaseMultiplier3);
        else if (other.CompareTag("purpleZone"))
            ApplyZoneDamage(healthDecreaseMultiplier4);

        if (other.CompareTag("enemy"))
        {
            ApplyZoneDamage(enemyDamageMultiplier);
            PlayDamageFeedback();
        }
    }

    private void ApplyZoneDamage(float amount)
    {
        health -= amount * Time.deltaTime;
        if (health < 0) health = 0;
        slider.value = health;
        bloodyScreen.DOFade((100 - health) / 100f, fadeDuration);
        Debug.Log("health: " + health);
    }

    public void AddHealth(int healthAmount)
    {
        health += healthAmount;
        if (health > 100) health = 100;
        slider.value = health;
        bloodyScreen.DOFade((100 - health) / 100f, fadeDuration);
        Debug.Log("current health: " + health);
    }

    public void SetHealth(int healthAmount)
    {
        health = healthAmount;
        slider.value = health;
        bloodyScreen.DOFade((100 - health) / 100f, 0);
    }

    private bool isTakingDamage = false;

    private void PlayDamageFeedback()
    {
        if (isTakingDamage) return;

        if (playerSprite != null)
        {
            isTakingDamage = true;
            Color originalColor = playerSprite.color;
            Color flashColor = new Color(1f, 0.4f, 0.4f, 1f); // soluk kırmızı
            playerSprite.color = flashColor;
            DOVirtual.DelayedCall(0.15f, () => 
            {
                playerSprite.color = originalColor;
                isTakingDamage = false;
            });
        }

        if (playerTransform != null)
        {
            playerTransform.DOShakePosition(0.2f, 0.1f, 10, 90, false, true);
        }
    }


}
