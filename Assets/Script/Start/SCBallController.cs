using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SCBallController : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 lastVelocity;
    float deceleration = 2f;
    public float increase = 4f;
    private bool iscolliding = false;
    public bool hasExpanded = false;
    private bool isStopped = false;
    private int randomNumber;
    private TextMeshPro textMesh;
    private bool hasBeenReleased = false; // ���� Ŭ���� �Ǿ����� ���θ� ����
    BGMControl bgmControl;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        bgmControl = FindObjectOfType<BGMControl>();

        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        randomNumber = Random.Range(1, 6);
        textMesh.text = randomNumber.ToString();
        textMesh.fontSize = 4;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero;
        textMesh.sortingOrder = 1;
        LaunchBall();

    }

    private void Update()
    {
        Move();
        expand();
    }

    void Move()
    {
        if (rigid == null || isStopped) return;

        lastVelocity = rigid.velocity;
        rigid.velocity -= rigid.velocity.normalized * deceleration * Time.deltaTime;

        if (rigid.velocity.magnitude <= 0.01f && hasExpanded)
        {
            isStopped = true;
            StartCoroutine(DestroyRigidbodyDelayed());
        }
    }
    void expand()
    {
        if (rigid == null || iscolliding) return;
        if (rigid.velocity.magnitude > 0.01f) return;
        if (Input.GetMouseButton(0)) return;

        if (!hasExpanded)
        {
            bgmControl.SoundEffectPlay(2);
        }
        transform.localScale += Vector3.one * increase * Time.deltaTime;
        hasExpanded = true;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!hasExpanded)
        {
            bgmControl.SoundEffectPlay(1);
        }

        if ((coll.gameObject.tag == "P1ball" || coll.gameObject.tag == "P2ball") && rigid == null)
        {
            if (randomNumber > 0)
            {
                randomNumber--;
                textMesh.text = randomNumber.ToString();
            }
            if (randomNumber <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (coll.contacts != null && coll.contacts.Length > 0)
        {
            Vector2 dir = Vector2.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
            if (rigid != null)
                rigid.velocity = dir * Mathf.Max(lastVelocity.magnitude, 0f); // �������� �ʰ� �ݻ縸 ����
        }
        this.iscolliding = true;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        this.iscolliding = false;
    }

    IEnumerator DestroyRigidbodyDelayed()
    {
        yield return new WaitForSeconds(0.8f);
        if (rigid != null)
            Destroy(rigid);
    }
    public void LaunchBall()
    {
        if (rigid != null)
        {
            rigid.velocity = SCGameManager.shotDirection * SCGameManager.shotDistance; // SCGameManager���� �� �����ͼ� ��ü �߻�
            hasBeenReleased = true; // ���� Ŭ���� �Ǿ����� ǥ��
        }
    }
}