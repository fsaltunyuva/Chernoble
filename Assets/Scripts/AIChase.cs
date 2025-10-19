using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed = 2f;
    public float distanceBetween;
    private float distance;
    public Animator animator;
    
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            // transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            animator.SetBool("chase", true);
        }
        else
        {
            animator.SetBool("chase", false);
        }
    }
}
