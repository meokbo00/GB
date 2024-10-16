using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossCenter : MonoBehaviour
{
    SPGameManager spGameManager;
    BGMControl bGMControl;
    Rigidbody2D rigid;
    public float increase = 4f;
    public bool hasExpanded = false;
    public int randomNumber;
    public int initialRandomNumber; // �ʱ� randomNumber ���� ������ ����
    public TextMeshPro textMesh;
    public BossFire[] bossfires; // ���� Enemy1Fire ������ ���� �迭
    public bool isShowHP;

    public int MaxHP;
    public int MinHP;
    public float MaxFireTime;
    public float MinFireTime;
    public float MaxAngle;
    public float MinAngle;
    public float fontsize;

    public GameObject[] Enemy;

    private void Start()
    {
        spGameManager = FindObjectOfType<SPGameManager>();
        bGMControl = FindObjectOfType<BGMControl>();
        rigid = GetComponent<Rigidbody2D>();
        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        randomNumber = Random.Range(MinHP, MaxHP);
        initialRandomNumber = randomNumber; // �ʱ� randomNumber ���� ����
        if (isShowHP)
        {
            textMesh.text = randomNumber.ToString();
        }
        textMesh.fontSize = fontsize;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero;
        textMesh.sortingOrder = 3;

        bossfires = GetComponentsInChildren<BossFire>(); // Enemy1Fire ������Ʈ �迭 ����

        StartCoroutine(RandomAttack());
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "P1ball" || coll.gameObject.tag == "P2ball" || coll.gameObject.tag == "P1Item" || coll.gameObject.tag == "P2Item"
            || (coll.gameObject.tag == "Item" && coll.gameObject.name != "SPEndlessF(Clone)"))
        {
            if (randomNumber > 0)
            {
                randomNumber--;
                if (isShowHP)
                {
                    textMesh.text = randomNumber.ToString();
                }
            }
            if (randomNumber <= 0)
            {
                bGMControl.SoundEffectPlay(4);
                spGameManager.RemoveEnemy();
                Destroy(transform.parent.gameObject); // �θ� ������Ʈ ����
            }
        }
        if (coll.gameObject.name == "SPTwiceF(Clone)")
        {
            randomNumber -= 1;
            if (randomNumber > 0)
            {
                textMesh.text = randomNumber.ToString();
            }
            if (randomNumber <= 0)
            {
                bGMControl.SoundEffectPlay(4);
                spGameManager.RemoveEnemy();
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator Attack1()
    {
        yield return new WaitForSeconds(Random.Range(MinFireTime, MaxFireTime));

        float targetAngle = Random.Range(MinAngle, MaxAngle);
        float currentAngle = transform.eulerAngles.z;
        float rotationTime = 1f; // ȸ���ϴ� �� �ɸ��� �ð�
        float elapsedTime = 0f;

        while (elapsedTime < rotationTime)
        {
            elapsedTime += Time.deltaTime;
            float angle = Mathf.LerpAngle(currentAngle, targetAngle, elapsedTime / rotationTime);
            transform.eulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        if (bossfires != null)
        {
            foreach (var enemy1Fire in bossfires)
            {
                spGameManager.AddBall();
                enemy1Fire.SpawnBullet();
            }
        }
    }

    private IEnumerator Attack2()
    {
        yield return new WaitForSeconds(7);

        if (Enemy.Length > 0)
        {
            GameObject enemy1 = Enemy[Random.Range(0, Enemy.Length)];
            GameObject enemy2 = Enemy[Random.Range(0, Enemy.Length)];
            GameObject enemy3 = Enemy[Random.Range(0, Enemy.Length)];
            GameObject enemy4 = Enemy[Random.Range(0, Enemy.Length)];


            Instantiate(enemy1, transform.position + new Vector3(-1.5f, 0, 0), Quaternion.identity);
            Instantiate(enemy2, transform.position + new Vector3(1.5f, 0, 0), Quaternion.identity);
            Instantiate(enemy3, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            Instantiate(enemy4, transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity);

        }
    }

    

    private IEnumerator RandomAttack()
    {
        while (true)
        {
            int randomAttack = Random.Range(0, 2);
            switch (randomAttack)
            {
                case 0:
                    yield return StartCoroutine(Attack1());
                    break;
                case 1:
                    yield return StartCoroutine(Attack2());
                    break;
            }

            yield return new WaitForSeconds(4f);
        }
    }
}
