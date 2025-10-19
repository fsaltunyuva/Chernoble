using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RotateIcon : MonoBehaviour
{
    [SerializeField] private Image icon; // Döndüreceğin UI Image
    [SerializeField] private float rotateSpeed = 180f; // derece/saniye

    void Start()
    {
        // Z ekseninde sürekli dönme
        icon.rectTransform
            .DORotate(new Vector3(0, 0, 360), 360f / rotateSpeed, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }
}