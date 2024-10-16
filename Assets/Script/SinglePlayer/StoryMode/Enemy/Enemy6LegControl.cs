using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy6LegControl : MonoBehaviour
{
    public float moveSpeed = 1f;  // �̵� �ӵ�
    public float rotationSpeed = 180f;  // ȸ�� �ӵ�
    private TextMeshPro textMesh;

    private Transform parentTransform;

    private void Start()
    {
        parentTransform = transform.parent;  // ���� ������Ʈ�� Transform�� ����
        StartCoroutine(MoveAndRotate());
    }

    private IEnumerator MoveAndRotate()
    {
        while (true)
        {
            float targetAngle = Random.Range(-70f, 70f);
            float currentAngle = parentTransform.eulerAngles.z;  // ���� ������Ʈ�� ���� ����
            float rotationTime = Mathf.Abs(targetAngle - currentAngle) / rotationSpeed;
            float elapsedTime = 0f;

            while (elapsedTime < rotationTime)
            {
                elapsedTime += Time.deltaTime;
                float angle = Mathf.LerpAngle(currentAngle, targetAngle, elapsedTime / rotationTime);
                parentTransform.eulerAngles = new Vector3(0, 0, angle);
                yield return null;
            }

            // �̵��� ���� ���� ��� (�ٶ󺸰� �ִ� �������� �̵�)
            Vector3 moveDirection = Random.Range(0, 2) == 0 ? -parentTransform.up : parentTransform.up;  // �θ� ������Ʈ�� ���� ���� �Ǵ� �Ʒ��� ���� �� �������� ����

            // 1.5~2.5�� ���� �̵�
            float moveTime = Random.Range(1.5f, 2.5f);
            elapsedTime = 0f;

            while (elapsedTime < moveTime)
            {
                elapsedTime += Time.deltaTime;
                parentTransform.position += moveDirection * moveSpeed * Time.deltaTime;
                yield return null;
            }

            // 1~5�� ���� ���� ���·� ���
            float waitTime = Random.Range(2f, 3f);
            yield return new WaitForSeconds(waitTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy6center enemy6Center = GetComponent<Enemy6center>();

        if (collision.gameObject.tag == "P1ball" || collision.gameObject.tag == "P2ball" || collision.gameObject.tag == "P1Item" || collision.gameObject.tag == "P2Item" || 
            (collision.gameObject.tag == "Item" && collision.gameObject.name != "SPEndlessF(Clone)"))
        {
            if (enemy6Center.randomNumber > 0)
            {
                enemy6Center.randomNumber--;
                textMesh.text = enemy6Center.randomNumber.ToString();
            }
            if (enemy6Center.randomNumber <= 0)
            {
                Destroy(transform.parent.gameObject); // �θ� ������Ʈ ����
            }
        }
    }
}