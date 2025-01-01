using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELHEnemyCenter : MonoBehaviour
{
    BGMControl bGMControl;
    ELGameManager elGameManager;
    public float radius;
    public float interval;
    public int segments = 50; // 원주 세그먼트 수
    private LineRenderer lineRenderer;

    void Start()
    {
        elGameManager = FindAnyObjectByType<ELGameManager>();
        bGMControl = FindObjectOfType<BGMControl>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        CreateCircle();
        StartCoroutine(IncrementRandomNumberRoutine());
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Gojung") return;

        if (coll.gameObject.CompareTag("P1ball") || coll.gameObject.CompareTag("P2ball") || coll.gameObject.CompareTag("P1Item") || coll.gameObject.CompareTag("P2Item") || (coll.gameObject.CompareTag("Item") && coll.gameObject.name != "SPEndlessF(Clone)"))
        {
            if (bGMControl.SoundEffectSwitch)
            {
                bGMControl.SoundEffectPlay(4);
            }
            elGameManager.RemoveEnemy();
            Destroy(gameObject);
        }
    }

    void CreateCircle()
    {
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }

    IEnumerator IncrementRandomNumberRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("EnemyCenter"))
                {
                    Enemy1center enemyCenter = hitCollider.GetComponent<Enemy1center>();
                    if (enemyCenter != null && enemyCenter.durability < enemyCenter.initialRandomNumber)
                    {
                        enemyCenter.durability++;

                        if (enemyCenter.isShowHP)
                        {
                            enemyCenter.textMesh.text = enemyCenter.durability.ToString();
                        }

                        Debug.Log($"Increased randomNumber for {hitCollider.name} to {enemyCenter.durability}");
                    }
                }
            }
        }
    }
}
