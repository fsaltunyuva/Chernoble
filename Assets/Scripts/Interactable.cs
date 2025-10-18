using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] ZoneDamage _zoneDamageScript;
    private bool isInRange = false;
    private Collider2D target;

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (target != null && target.CompareTag("health"))
            {
                Destroy(target.gameObject);
                _zoneDamageScript.AddHealth(30);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("health"))
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
