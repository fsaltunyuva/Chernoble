using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HeartBeat : MonoBehaviour
{
    [SerializeField] private RectTransform heart; // UI Image’in RectTransform
    [SerializeField] private float amplitude = 10f; // Yukarı-aşağı mesafe
    [SerializeField] private float speed = 1f; // 1 tur için süre (saniye)

    private float originalY;

    void Start()
    {
        heart = GetComponent<RectTransform>();
        if (heart == null) return;

        originalY = heart.anchoredPosition.y;

        // Yukarı-aşağı sallama
        heart.DOAnchorPosY(originalY + amplitude, speed)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine); // sinüs gibi yumuşak hareket
    }
}