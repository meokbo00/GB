using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject[] spherePrefabs; 
    public GameObject background; 
    public float minSpawnTime = 7f; 
    public float maxSpawnTime = 12f; 

    private float nextSpawnTime;
    private Collider2D backgroundCollider;
    BGMControl bGMControl;

    void Start()
    {
        bGMControl = FindObjectOfType<BGMControl>();
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        backgroundCollider = background.GetComponent<Collider2D>();
    }
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnSphere(); 
            nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
    void SpawnSphere()
    {
        Vector2 min = backgroundCollider.bounds.min;
        Vector2 max = backgroundCollider.bounds.max;
        Vector3 randomPosition = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0f);

        int prefabIndex = Random.Range(0, spherePrefabs.Length);
        Instantiate(spherePrefabs[prefabIndex], randomPosition, Quaternion.identity);
        bGMControl.SoundEffectPlay(2);
    }
}