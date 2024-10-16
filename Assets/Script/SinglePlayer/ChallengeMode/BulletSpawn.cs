using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject[] bullets; 
    public float spawnInterval = 10f; 
    public float minimumSpawnInterval = 1f; 
    public float minForce = 1.7f; 
    public float maxForce = 7f;

    private BoxCollider2D backgroundCollider;
    private float timer;

    void Start()
    {
        GameObject background = GameObject.Find("BackGround");
        if (background != null)
        {
            backgroundCollider = background.GetComponent<BoxCollider2D>();
        }

        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnBullet();
            spawnInterval = Mathf.Max(minimumSpawnInterval, spawnInterval - 0.2f);
            timer = spawnInterval; 
        }
    }

    void SpawnBullet()
    {
        float x = Random.Range(backgroundCollider.bounds.min.x, backgroundCollider.bounds.max.x);
        float y = Random.Range(backgroundCollider.bounds.min.y, backgroundCollider.bounds.max.y);
        Vector2 spawnPosition = new Vector2(x, y);

        int randomIndex = Random.Range(0, bullets.Length);
        GameObject bulletPrefab = bullets[randomIndex];

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 forceDirection = Random.insideUnitCircle.normalized;
            float forceMagnitude = Random.Range(minForce, maxForce);
            rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
        }
    }
}