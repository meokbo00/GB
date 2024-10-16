using UnityEngine;

public class SPRandomGenerate : MonoBehaviour
{
    BGMControl bGMControl;
    public GameObject[] spherePrefabs;
    public GameObject background;
    public float minSpawnTime = 7f;
    public float maxSpawnTime = 12f;

    private float nextSpawnTime;
    private Collider2D backgroundCollider;
    private StageGameManager stageGameManager;

    void Start()
    {
        bGMControl = FindObjectOfType<BGMControl>();
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        backgroundCollider = background.GetComponent<Collider2D>();

        stageGameManager = FindObjectOfType<StageGameManager>();
        if (stageGameManager == null)
        {
            Debug.LogError("StageGameManager�� ã�� �� �����ϴ�.");
        }
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

        int maxIndex;

        if (stageGameManager.StageClearID <= 6)
        {
            return;
        }
        else if (stageGameManager.StageClearID <= 10)
        {
            maxIndex = 0;
        }
        else if (stageGameManager.StageClearID <= 17)
        {
            maxIndex = 1;
        }
        else if (stageGameManager.StageClearID <= 25)
        {
            maxIndex = 2;
        }
        else if (stageGameManager.StageClearID <= 32)
        {
            maxIndex = 3;
        }
        else if (stageGameManager.StageClearID <=44)
        {
            maxIndex = 4;
        }
        else if(stageGameManager.StageClearID == 45)
        {
            maxIndex = 3;
        }
        else if(stageGameManager.StageClearID == 65)
        {
            maxIndex = 3;
        }
        else
        {
            maxIndex = 4;
        }

        int prefabIndex = Random.Range(0, maxIndex);
        bGMControl.SoundEffectPlay(2);
        Instantiate(spherePrefabs[prefabIndex], randomPosition, Quaternion.identity);
    }
}