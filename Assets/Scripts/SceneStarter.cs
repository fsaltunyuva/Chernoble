using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SceneStarter : MonoBehaviour
{
    [SerializeField] Image blackScreen;
    [SerializeField] private float fadeDuration = 5f;
    
    void Start()
    {
        blackScreen.DOFade(0, fadeDuration).OnComplete(() =>
        {
            Singleton.Instance.isMovementEnabled = true;
        });
    }
}
