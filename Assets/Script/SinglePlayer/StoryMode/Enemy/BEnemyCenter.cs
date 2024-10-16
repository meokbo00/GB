using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BEnemyCenter : MonoBehaviour
{
    SPGameManager spGameManager;
    BGMControl bGMControl;
    Rigidbody2D rigid;
    public float increase = 4f;
    public bool hasExpanded = false;
    public int randomNumber;
    public int initialRandomNumber; // �ʱ� randomNumber ���� ������ ����
    public TextMeshPro textMesh;
    public Enemy1Fire[] enemy1Fires; // ���� Enemy1Fire ������ ���� �迭
    public bool isShowHP;
    public bool isHide;

    public int MaxHP;
    public int MinHP;
    public float MaxFireTime;
    public float MinFireTime;
    public float MaxAngle;
    public float MinAngle;
    public float fontsize;

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

        enemy1Fires = GetComponentsInChildren<Enemy1Fire>(); // Enemy1Fire ������Ʈ �迭 ����

        StartCoroutine(RotateObject());

        if (isHide)
        {
            ChangeObjectColor(transform);
        }
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
        if (coll.gameObject.name == "Invincible(Clone)")
        {
            randomNumber -= 3;
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
    private IEnumerator RotateObject()
    {
        while (true)
        {
            // 5�� ���� ����
            yield return new WaitForSeconds(Random.Range(MinFireTime, MaxFireTime));

            // ȸ���� ���� ����
            float targetAngle = Random.Range(MinAngle, MaxAngle);
            float currentAngle = transform.eulerAngles.z;
            float rotationTime = 1f; // ȸ���ϴ� �� �ɸ��� �ð�
            float elapsedTime = 0f;

            // ȸ���ϱ�
            while (elapsedTime < rotationTime)
            {
                elapsedTime += Time.deltaTime;
                float angle = Mathf.LerpAngle(currentAngle, targetAngle, elapsedTime / rotationTime);
                transform.eulerAngles = new Vector3(0, 0, angle);
                yield return null;
            }

            // ȸ���� ���� �� 1�� �ڿ� �Ѿ� �߻�
            yield return new WaitForSeconds(1f);

            if (enemy1Fires != null)
            {
                foreach (var enemy1Fire in enemy1Fires)
                {
                    spGameManager.AddBall();
                    enemy1Fire.SpawnBullet();
                }
            }
        }
    }

    private void ChangeObjectColor(Transform parentTransform)
    {
        if (!isHide)
            return;

        // �ڱ� �ڽ��� ���� ����
        SpriteRenderer currentSpriteRenderer = parentTransform.GetComponent<SpriteRenderer>();
        if (currentSpriteRenderer != null)
        {
            GameObject background = GameObject.Find("BackGround");
            if (background != null)
            {
                SpriteRenderer backgroundSpriteRenderer = background.GetComponent<SpriteRenderer>();
                if (backgroundSpriteRenderer != null)
                {
                    currentSpriteRenderer.color = backgroundSpriteRenderer.color;
                }
            }
        }

        // �θ� ������Ʈ�� �ڽĵ��� �˻��ϸ鼭 ������ ����
        foreach (Transform child in parentTransform)
        {
            ChangeObjectColor(child);
        }
    }
}