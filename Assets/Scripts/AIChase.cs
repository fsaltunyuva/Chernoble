using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AIChase : MonoBehaviour
{
    [Header("Chase Settings")]
    public GameObject player;
    public float speed = 2f;
    public float distanceBetween = 5f;

    [Header("Health Settings")]
    public int health = 100;
    private bool isDead = false;

    [Header("Components")]
    public Animator animator;
    private SpriteRenderer sr;

    // private void Awake()
    // {
    //     sr = GetComponent<SpriteRenderer>();
    // }
    //
    // void Update()
    // {
    //     if (isDead) return; // öldüyse hiçbir şey yapma
    //
    //     if (player == null) return;
    //
    //     float distance = Vector2.Distance(transform.position, player.transform.position);
    //     Vector2 direction = player.transform.position - transform.position;
    //     direction.Normalize();
    //
    //     if (distance < distanceBetween)
    //     {
    //         transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    //         animator?.SetBool("chase", true);
    //     }
    //     else
    //     {
    //         animator?.SetBool("chase", false);
    //     }
    // }
    
    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() // physics için FixedUpdate kullan
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(rb.position, player.transform.position);
        if (distance < distanceBetween)
        {
            Vector2 direction = ((Vector2)player.transform.position - rb.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            animator?.SetBool("chase", true);
        }
        else
        {
            animator?.SetBool("chase", false);
        }
    }


    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(DieAfterDelay(0.2f)); // feedback tamamlanana kadar bekle
        }
    }

    private IEnumerator DieAfterDelay(float delay)
    {
        isDead = true;

        // Chase animasyonunu kapat
        animator?.SetBool("chase", false);

        yield return new WaitForSeconds(delay);

        // Ölüm efekti (fade out + scale küçülme)
        Sequence seq = DOTween.Sequence();
        seq.Append(sr.DOFade(0, 0.4f));
        seq.Join(transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack));
        seq.AppendCallback(() => Destroy(gameObject));
    }
    
    public void PlayDamageFeedback()
    {
        StartCoroutine(DamageFeedbackRoutine());
    }

    private IEnumerator DamageFeedbackRoutine()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        Color originalColor = sr.color;
        sr.color = Color.red;

        Vector3 originalPos = transform.localPosition;
        transform.DOShakePosition(0.2f, 0.1f, 10, 90, false, true)
            .OnComplete(() => transform.localPosition = originalPos);

        yield return new WaitForSeconds(0.15f);
        sr.color = originalColor;
    }

}