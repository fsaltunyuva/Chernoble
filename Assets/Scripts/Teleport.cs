using UnityEngine;
using DG.Tweening;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject teleportGameObject;
    [SerializeField] GameObject background;
    [SerializeField] GameObject stopPoint;
    public bool isSuccessful = false;
    public int moveDir = 1;
    Vector3 move;

    void Update()
    {
        move = new Vector3(moveDir * Time.deltaTime, 0, 0);
        transform.position += move;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("rightCollider"))
        {
            moveDir = -1;
        }
        else if (other.CompareTag("leftCollider"))
        {
            moveDir = 1;
        }
    }
}
