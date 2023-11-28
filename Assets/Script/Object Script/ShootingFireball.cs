using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingFireball : MonoBehaviour
{
    public GameObject projectile;
    public AudioSource Shooting;
    public float projectileLifetime = 1.0f;

    public string Space = "PressedSpace";

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool(Space, true);
            // Get horizontal and vertical input
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", verticalInput);

            // Determine the direction of the shot based on horizontal and vertical input
            if (horizontalInput > 0)
            {
                // Shoot to the right
                Shooting.Play();
                ShootProjectile(new Vector2(5.0f, 0.0f));
            }
            else if (horizontalInput < 0)
            {
                // Shoot to the left
                Shooting.Play();
                ShootProjectile(new Vector2(-5.0f, 0.0f));
            }
            else if (verticalInput > 0)
            {
                // Shoot up
                Shooting.Play();
                ShootProjectile(new Vector2(0.0f, 5.0f));
            }
            else if (verticalInput < 0)
            {
                // Shoot down
                Shooting.Play();
                ShootProjectile(new Vector2(0.0f, -5.0f));
            }
        }else{
            animator.SetBool(Space, false);
        }
    }

    void ShootProjectile(Vector2 velocity)
    {
        GameObject fireball = Instantiate(projectile, transform.position, Quaternion.identity);
        Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();

        fireballRb.velocity = velocity;

        if (velocity.x > 0.0f)
        {
            fireball.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
        }
        else if (velocity.x < 0.0f)
        {
            fireball.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        }
        else if (velocity.y > 0.0f)
        {
            fireball.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else if (velocity.y < 0.0f)
        {
            fireball.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -180.0f);
        }

        // Start a coroutine to destroy the projectile after a certain amount of time
        StartCoroutine(DestroyProjectileAfterDelay(fireball, projectileLifetime));
    }

    IEnumerator DestroyProjectileAfterDelay(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(projectile);
    }
}