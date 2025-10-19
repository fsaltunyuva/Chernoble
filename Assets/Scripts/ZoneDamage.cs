using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ZoneDamage : MonoBehaviour
{
    [SerializeField] public int healthDecreaseMultiplier1;
    [SerializeField] public int healthDecreaseMultiplier2;
    [SerializeField] public int healthDecreaseMultiplier3;
    [SerializeField] public int healthDecreaseMultiplier4;
    [SerializeField] public int enemyDamageMultiplier;
    private float health = 100f;
    [SerializeField] Slider slider;

    [SerializeField] Image bloodyScreen;
    [SerializeField] private float fadeDuration = 1f;

    [Header("Player Feedback")]
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private TextMeshProUGUI radiationText;
    
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] private GameObject glitchEffectA;
    [SerializeField] private GameObject glitchEffectB;
    [SerializeField] private GameObject glitchEffectC;
    [SerializeField] private GameObject glitchEffectD;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("greenZone"))
        {
            glitchEffectA.GetComponent<Image>().DOFade(1, 1.5f);
        }
        else if (other.CompareTag("blueZone"))
        {
            glitchEffectB.GetComponent<Image>().DOFade(1, 1.5f);
        }
        else if (other.CompareTag("redZone"))
        {
            glitchEffectC.GetComponent<Image>().DOFade(1, 1.5f);
        }
        else if (other.CompareTag("purpleZone"))
        {
            glitchEffectD.GetComponent<Image>().DOFade(1, 1.5f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("greenZone"))
        {
            ApplyZoneDamage(healthDecreaseMultiplier1, Color.green);
            // glitchEffectA.GetComponent<Image>().DOFade(1, 1.5f);
            // SetGlitchFade(glitchEffectA, 1f, 0.6f); // fade-in
        }
        else if (other.CompareTag("blueZone"))
        {
            // glitchEffectB.GetComponent<Image>().DOFade(1, 1.5f);
            ApplyZoneDamage(healthDecreaseMultiplier2, Color.blue);
            // SetGlitchFade(glitchEffectB, 1f, 0.6f);
        }
        else if (other.CompareTag("redZone"))
        {
            // glitchEffectC.GetComponent<Image>().DOFade(1, 1.5f);
            ApplyZoneDamage(healthDecreaseMultiplier3, Color.red);
            // SetGlitchFade(glitchEffectC, 1f, 0.6f);
        }
        else if (other.CompareTag("purpleZone"))
        {
            // glitchEffectD.GetComponent<Image>().DOFade(1, 1.5f);
            ApplyZoneDamage(healthDecreaseMultiplier4, new Color(0.5f, 0f, 0.5f));
            // SetGlitchFade(glitchEffectD, 1f, 0.6f);
        }
        if (other.CompareTag("enemy"))
        {
            ApplyZoneDamage(enemyDamageMultiplier, Color.black, false);
            PlayDamageFeedback();
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("greenZone"))
            glitchEffectA.GetComponent<Image>().DOFade(0, 1f);
        else if (other.CompareTag("blueZone"))
            glitchEffectB.GetComponent<Image>().DOFade(0, 1f);
        else if (other.CompareTag("redZone"))
            glitchEffectC.GetComponent<Image>().DOFade(0, 1f);
        else if (other.CompareTag("purpleZone"))
            glitchEffectD.GetComponent<Image>().DOFade(0, 1f);

        radiationText.text = "0";
        radiationText.color = new Color(254, 210, 0);
    }


    private void ApplyZoneDamage(float amount, Color color, bool isDamageFromZone = true)
    {
        health -= amount * Time.deltaTime;
        if (health < 0)
        {
            health = 0;
            gameOverPanel.SetActive(true);
        }
        slider.value = health;
        bloodyScreen.DOFade((100 - health) / 100f, fadeDuration);

        if (isDamageFromZone)
        {
            radiationText.text = amount.ToString();
            radiationText.color = color;
        }
    }

    public void AddHealth(int healthAmount)
    {
        health += healthAmount;
        if (health > 100) health = 100;
        slider.value = health;
        bloodyScreen.DOFade((100 - health) / 100f, fadeDuration);
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
    
    public void SetMaxHealth(int value)
    {
        health = value;
        slider.maxValue = value;
        slider.value = health;
    }
    
    private void SetGlitchFade(GameObject glitchObj, float targetAlpha, float duration)
    {
        SpriteRenderer sr = glitchObj.GetComponent<SpriteRenderer>();

        // Aktif değilse aç
        if (!glitchObj.activeSelf)
        {
            glitchObj.SetActive(true);
            Debug.Log("glitchObj active");
        }
        
        // Fade animasyonu başlat
        sr.DOFade(targetAlpha, duration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                // Tamamen şeffaf olduysa objeyi kapat
                if (Mathf.Approximately(targetAlpha, 0f))
                    glitchObj.SetActive(false);
            });
    }

    
}
