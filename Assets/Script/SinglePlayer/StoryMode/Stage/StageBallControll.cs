using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StageBallController : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject StageStart;
    private StageGameManager stageGameManager;
    private StageBallManager stageBallManager;
    private bool hasbeenout = false;
    public static int chooseStage;

    private float dragAmount = 1.1f;
    bool hasBeenLaunched = false;
    bool isStopped = false; // 공이 완전히 멈췄는지 여부
    private float decelerationThreshold = 0.4f;
    public PhysicsMaterial2D bouncyMaterial;

    private float randomX;
    private float randomY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stageBallManager = FindAnyObjectByType<StageBallManager>();
        stageGameManager = FindAnyObjectByType<StageGameManager>();
        rb.drag = 0f;

        if (stageGameManager.StageClearID <= 6)
        {
            randomX = 0;
            randomY = -5;
        }
        else if (stageGameManager.StageClearID >= 7)
        {
            // 방금 저장한 Player의 위치값을 불러옴
            randomX = GlobalData.PlayerPosition.x;
            randomY = GlobalData.PlayerPosition.y;
        }

        gameObject.transform.position = new Vector3(randomX, randomY, gameObject.transform.position.z);

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null && bouncyMaterial != null)
        {
            collider.sharedMaterial = bouncyMaterial;
        }
    }

    private void Update()
    {
        if (!stageBallManager.isDragging)
        {
            LaunchBall();
        }
        if (hasBeenLaunched && !isStopped)
        {
            SlowDownBall();
        }
    }
    void LaunchBall()
    {
        Vector2 launchForce = StageBallManager.shotDirection * (StageBallManager.shotDistance * 1.4f);
        rb.AddForce(launchForce, ForceMode2D.Impulse);
        stageBallManager.isDragging = true;
        rb.drag = dragAmount;
        hasBeenLaunched = true;
    }
    void SlowDownBall()
    {
        if (rb == null) return;

        if (rb.velocity.magnitude <= decelerationThreshold)
        {
            rb.velocity = Vector2.zero;
            isStopped = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TextManager textManager = FindObjectOfType<TextManager>();
        if (hasbeenout) return;
        if (collision.gameObject.tag == "StageDeadZone")
        {
            textManager.GiveMeTextId(1);
            Debug.Log("대화창이 활성화 되었습니다");
            hasbeenout = true;
        }
    }
}
