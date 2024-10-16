using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SPGameManager : MonoBehaviour
{
    public GameObject P1ballPrefab;
    public GameObject[] FireItemPrefab;
    public GameObject P1firezone;
    public GameObject P1Itemsave;
    public GameObject fireitem;

    private Vector3 clickPosition;
    private StageGameManager gameManager;
    public bool isDragging = false;
    public static float shotDistance;
    public static Vector3 shotDirection;
    public int chooseStagenum;
    private int totalBalls = 0;
    private int totalEnemies;


    private void Start()
    {
        gameManager = FindAnyObjectByType<StageGameManager>();
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        P1firezone.gameObject.SetActive(true);
        Debug.Log(StageState.chooseStage);
    }
    public void PrintDestroyedicontag(string icontag)
    {
        this.fireitem = null;
        switch (icontag)
        {
            case "Item_Big": fireitem = FireItemPrefab[0]; break;
            case "Item_Small": fireitem = FireItemPrefab[1]; break;
            case "Item_Twice": fireitem = FireItemPrefab[2]; break;
            case "Item_Endless": fireitem = FireItemPrefab[3]; break;
            case "Item_Invincible": fireitem = FireItemPrefab[4]; break;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;
            Collider2D[] colliders = Physics2D.OverlapPointAll(clickPosition);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject == P1firezone)
                {
                    if (fireitem != null)
                    {
                        Instantiate(fireitem, clickPosition, Quaternion.identity);
                        AddBall();
                        Debug.Log("P1이 아이템을 사용하였습니다");
                        Debug.Log("아이템의 이름은 " + fireitem.gameObject.name + "입니다");
                        isDragging = true;
                        fireitem = null;
                        break;
                    }
                    else
                    {
                        Instantiate(P1ballPrefab, clickPosition, Quaternion.identity);
                        AddBall();
                        Debug.Log("P1이 기본구체를 날렸습니다");
                        isDragging = true;
                        break;
                    }
                }
            }
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;
            shotDistance = Vector3.Distance(clickPosition, currentPosition)*2;
            Vector3 dragDirection = (currentPosition - clickPosition).normalized;
            shotDirection = dragDirection;
            isDragging = false;
        }
    }
    public void AddBall()
    {
        totalBalls++;
        CheckBallLimit();
    }

    public void RemoveBall()
    {
        totalBalls--;
        CheckBallLimit();
    }

    private void CheckBallLimit()
    {
        if (totalBalls > 16)
        {
            SceneManager.LoadScene("Fail");
        }
    }
    public void RemoveEnemy()
    {
        totalEnemies--;
        if (totalEnemies <= 0)
        {
            StageClear();
        }
    }

    private void StageClear()
    {
        if (gameManager.StageClearID == StageState.chooseStage && gameManager.StageClearID != 5)
        {
            gameManager.StageClearID += 1;
            gameManager.SaveStageClearID();
        }
        if (gameManager.StageClearID == 5)
        {
            gameManager.StageClearID += 0.5f;
            gameManager.SaveStageClearID();
        }
        SceneManager.LoadScene("Clear");
    }
}