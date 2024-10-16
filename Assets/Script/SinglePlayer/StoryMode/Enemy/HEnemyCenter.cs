using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class HEnemyCenter : MonoBehaviour
{
    BGMControl bGMControl;
    public float radius;
    public float interval;
    public int segments = 50; // ���� �׸� �� ����� ���׸�Ʈ ��
    private LineRenderer lineRenderer;

    void Start()
    {
        bGMControl = FindObjectOfType<BGMControl>();
        // LineRenderer ������Ʈ�� �ʱ�ȭ
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
        if (coll.gameObject.tag == "P1ball" || coll.gameObject.tag == "P2ball" || coll.gameObject.tag == "P1Item" || coll.gameObject.tag == "P2Item" || (coll.gameObject.tag == "Item" && coll.gameObject.name != "SPEndlessF(Clone)"))
        {
            bGMControl.SoundEffectPlay(4);

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
            // interval ��ŭ ���
            yield return new WaitForSeconds(interval);

            // �ݰ� �ȿ� �ִ� ��� ������Ʈ�� ������
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("EnemyCenter"))
                {
                    Enemy1center enemyCenter = hitCollider.GetComponent<Enemy1center>();

                    if (enemyCenter != null)
                    {
                        if (enemyCenter.randomNumber < enemyCenter.initialRandomNumber)
                        {
                            enemyCenter.randomNumber++;

                            if (enemyCenter.isShowHP)
                            {
                                enemyCenter.textMesh.text = enemyCenter.randomNumber.ToString();
                            }

                            // ����� �޽���: randomNumber ����
                            Debug.Log($"Increased randomNumber for {hitCollider.name} to {enemyCenter.randomNumber}");
                        }
                    }
                }
            }
        }
    }
}