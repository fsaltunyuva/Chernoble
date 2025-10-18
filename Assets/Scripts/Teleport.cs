using UnityEngine;
using DG.Tweening;

public class Teleport : MonoBehaviour
{
    // [SerializeField] GameObject teleportSlider;
    [SerializeField] GameObject stopPoint;
    [SerializeField] GameObject movingPart;
    public bool isSuccessful = false;

    [SerializeField] float moveDistance;
    [SerializeField] float moveDuration;
    Tween tween;

    private bool isInRange = false;
    private Collider2D target;

    GameObject teleporter;

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
                Singleton.Instance.isInputEnabled = true;
                Destroy(teleporter);
            }
        }
        else if (!isInRange && Input.GetKeyDown(KeyCode.E))
        {
            isSuccessful = false;
            Debug.Log("fail!");
            Singleton.Instance.isInputEnabled = true;
            Destroy(teleporter);
        }
    }

    public void GetInteractedTeleporter(GameObject obj)
    {
        teleporter = obj;
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
