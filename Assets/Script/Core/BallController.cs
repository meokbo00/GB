using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 lastVelocity;
    float deceleration = 2f;
    public float increase = 4f;
    private bool iscolliding = false;
    public bool hasExpanded = false;
    private bool isStopped = false;
    private int randomNumber;
    private bool hasBeenReleased = false;
    BGMControl bGMControl;
    SPGameManager spGameManager;

    private void Start()
    {
        spGameManager = FindAnyObjectByType<SPGameManager>();
        bGMControl = FindAnyObjectByType<BGMControl>();
        rigid = GetComponent<Rigidbody2D>();
        randomNumber = Random.Range(1, 6);
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && rigid != null && !hasBeenReleased)
        {
            rigid.velocity = SPGameManager.shotDirection * SPGameManager.shotDistance;
            hasBeenReleased = true;
        }
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
        if (rigid.velocity.magnitude > 0.1f) return;
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

        if ((coll.gameObject.tag == "P1ball" || coll.gameObject.tag == "P2ball" || coll.gameObject.tag == "P1Item" || coll.gameObject.tag == "P2Item"
            || coll.gameObject.tag == "EnemyBall" || coll.gameObject.tag == "Item") && rigid == null)
        {
            if (randomNumber > 1)
            {
                randomNumber--;  // randomNumber 감소
            }
            else if (randomNumber <= 1)  // randomNumber가 1 이하일 때 제거
            {
                spGameManager.RemoveBall();
                Destroy(gameObject);
            }
        }

        if ((coll.gameObject.name == "SPTwiceF(Clone)" && rigid == null) || (coll.gameObject.name == "TwiceBullet(Clone)" && rigid == null))
        {
            if (randomNumber > 1)
            {
                randomNumber--;
            }
            else if (randomNumber <= 1)
            {
                spGameManager.RemoveBall();
                Destroy(gameObject);
            }
        }

        if (coll.contacts != null && coll.contacts.Length > 0)
        {
            Vector2 dir = Vector2.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
            if (rigid != null)
                rigid.velocity = dir * Mathf.Max(lastVelocity.magnitude, 0f); // 반사만 적용
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
}
