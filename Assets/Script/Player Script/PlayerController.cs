using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myRB;
    public float speed = 5f;

    public Animator animator;
    public AudioSource Walk;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) {
            if (!Walk.isPlaying) {
                Walk.Play();
            }
        } else {
            Walk.Stop();
        }
    }

    void FixedUpdate()
    {
        myRB.MovePosition(myRB.position + movement * speed * Time.fixedDeltaTime);
    }
    
}