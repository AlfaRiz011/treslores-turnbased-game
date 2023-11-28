using UnityEngine;

public class TrapShot : MonoBehaviour
{
    public float fireRate = 5f;
    public float projectileVelocity = 8f;

    public GameObject projectilePrefab;

    private float timer;

    void Start()
    {
        timer = fireRate;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = fireRate;

            // Spawn the projectile.
            var projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.position;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileVelocity);
        }
    }
}