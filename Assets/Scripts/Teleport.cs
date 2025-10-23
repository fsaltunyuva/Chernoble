using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject stopPoint;
    [SerializeField] GameObject movingPart;
    public bool isSuccessful = false;

    [SerializeField] float moveDistance;
    [SerializeField] float moveDuration;


    private bool isInRange = false;
    private Collider2D target;

    GameObject teleporter;
    GameObject player;

    [SerializeField] Image blackScreen;
    [SerializeField] private float fadeDuration = 2.5f;

    [SerializeField] ZoneDamage _zoneDamageScript;

    void Start()
    {
        StartCursorMove();
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (target != null && target.CompareTag("middleCollider"))
            {
                isSuccessful = true;
                Debug.Log("success!");
                Singleton.Instance.isMovementEnabled = true;

                SingletonMusic.Instance.PlaySFX("teleport1_SFX");
                
                Sequence seq = DOTween.Sequence();

                seq.Append(blackScreen.DOFade(1, fadeDuration));
                seq.AppendCallback(() => player.transform.position = new Vector3(1.07f, -0.97f, 0f));
                seq.AppendCallback(() => _zoneDamageScript.SetHealth(_zoneDamageScript.GetMaxHealth()));
                seq.AppendCallback(() => Singleton.Instance.gunAmmo = Singleton.Instance.maxAmmo);
                seq.AppendCallback(() => Singleton.Instance.UpdateAmmoText());
                seq.AppendCallback(() => SingletonMusic.Instance.PlaySFX("teleport2_SFX"));
                seq.Append(blackScreen.DOFade(0, fadeDuration));

                Destroy(teleporter);
            }
        }
        else if (!isInRange && Input.GetKeyDown(KeyCode.E))
        {
            isSuccessful = false;
            Debug.Log("fail!");
            Singleton.Instance.isMovementEnabled = true;
            Destroy(teleporter);
        }
    }

    public void GetInteractedTeleporter(GameObject obj, GameObject playerGameObject)
    {
        teleporter = obj;
        player = playerGameObject;
    }

    void StartCursorMove()
    {
        Vector3 targetPos = new Vector3(movingPart.transform.position.x + moveDistance, movingPart.transform.position.y, movingPart.transform.position.z);

        movingPart.transform.DOMove(targetPos, moveDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("middleCollider"))
        {
            isInRange = true;
            target = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == target)
        {
            isInRange = false;
            target = null;
        }
    }
}
