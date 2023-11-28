using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamLeader : MonoBehaviour
{
    public Transform target;
    public float walkingSpeed = 3f;
    public float followDistance = 2f;

    private Animator animator;
    private Vector3 lastTargetPosition;
    private Vector2 movement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        lastTargetPosition = target.position;
    }

    private void Update()
    {
        Vector3 targetPosition = target.position;

        if (Vector3.Distance(transform.position, targetPosition) > followDistance)
        {
            // Mengkalkulasi arah gerakan target
            movement = targetPosition - transform.position;
            movement.Normalize();

            // menggerakkan karakter ke target
            transform.position += (Vector3)movement * walkingSpeed * Time.deltaTime;


            // Menjalankan animasi karakter
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.magnitude);

            // Memperbarui posisi terakhir target
            lastTargetPosition = targetPosition;
        }
        else
        {
            // berhenti bergerak
            movement = Vector2.zero;
            animator.SetFloat("Speed", 0f);
        }

        // Untuk melakukan pengecekan apakah target bergerak atau tidak
        if (lastTargetPosition != targetPosition)
        {
            Vector2 facingDirection = targetPosition - transform.position;
            facingDirection.Normalize();
        }
    }
}
