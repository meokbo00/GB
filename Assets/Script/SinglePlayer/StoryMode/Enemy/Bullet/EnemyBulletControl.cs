using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBulletControl : MonoBehaviour
{
    SPGameManager spGameManager;
    Rigidbody2D rb;
    BGMControl bGMControl;
    Vector2 lastVelocity;
    float deceleration = 2f;
    public float increase;
    private bool iscolliding = false;
    public bool hasExpanded = false;
    private bool isStopped = false;
    private int durability;
    private TextMeshPro textMesh;
    public float fontsize;
    public int BallMinHP = 1;
    public int BallMaxHP = 6;
    public PhysicsMaterial2D bouncyMaterial;
    private Vector3 initialScale; // 초기 공 크기
    private Vector3 targetScale; // 목표 크기
    private const string SPTwiceFName = "SPTwiceF(Clone)";
    private const string TwiceBulletName = "TwiceBullet(Clone)";
    private const string GojungTag = "Gojung";
    private const string WallTag = "Wall";
    private const string EnemyCenterTag = "EnemyCenter";



    private void Start()
    {
        spGameManager = FindAnyObjectByType<SPGameManager>();
        bGMControl = FindAnyObjectByType<BGMControl>();
        rb = GetComponent<Rigidbody2D>();
        GameObject textObject = new GameObject("TextMeshPro");

        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        durability = Random.Range(BallMinHP, BallMaxHP);
        textMesh.text = durability.ToString();
        textMesh.fontSize = fontsize;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero;
        textMesh.sortingOrder = 1;

        rb.drag = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null && bouncyMaterial != null)
        {
            collider.sharedMaterial = bouncyMaterial;
        }

        //initialScale = transform.localScale;
    }

    private void Update()
    {
        Move();
        expand();
    }

    void Move()
    {
        if (rb == null || isStopped) return;

        lastVelocity = rb.velocity;
        rb.velocity -= rb.velocity.normalized * deceleration * Time.deltaTime;

        if (rb.velocity.magnitude <= 0.01f && hasExpanded)
        {
            isStopped = true;
        }
    }

    void expand()
    {
        if (rb == null || iscolliding) return;
        if (rb.velocity.magnitude > 0.1f) return;
        if (Input.GetMouseButton(0)) return;

        if (!hasExpanded)
        {
            bGMControl.SoundEffectPlay(1);
        }
        transform.localScale += Vector3.one * increase * Time.deltaTime;
        hasExpanded = true;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!hasExpanded)
        {
            bGMControl.SoundEffectPlay(0);
        }
        if (!coll.collider.isTrigger && hasExpanded)
        {
            hasExpanded = true; // 팽창 중단
            transform.localScale = transform.localScale; // 현재 크기에서 멈춤
            DestroyRigidbody(); // Rigidbody 제거
        }
        if (coll.gameObject.name == "SPInvincibleF(Clone)")
        {
            ChallengeGameManager chmanager = FindObjectOfType<ChallengeGameManager>();
            chmanager.scorenum++;
            Destroy(gameObject);
        }

        if ((coll.collider.name != SPTwiceFName || coll.collider.name != TwiceBulletName) && rb == null)
        {
            if (coll.collider.CompareTag(GojungTag)) return;
            if (coll.collider.CompareTag(WallTag)) return;
            if (coll.collider.CompareTag(EnemyCenterTag)) return;

            TakeDamage(1);
            textMesh.text = durability.ToString();
        }
        if ((coll.collider.name == SPTwiceFName || coll.collider.name == TwiceBulletName) && rb == null)
        {
            TakeDamage(2);
            textMesh.text = durability.ToString();
        }
        this.iscolliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        this.iscolliding = false;
    }
    void TakeDamage(int damage)
    {
        durability -= damage;
        if (durability <= 0)
        {
            ChallengeGameManager chmanager = FindObjectOfType<ChallengeGameManager>();
            if (SceneManager.GetActiveScene().name == "ChallengeScene")
            {
                chmanager.scorenum++;
            }
            spGameManager.RemoveBall();
            Destroy(gameObject);
        }
    }

    void DestroyRigidbody()
    {
        if (rb != null)
        {
            Destroy(rb);
            rb = null;
        }
    }
}