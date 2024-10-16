using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    StageGameManager stageGameManager;
    public GameObject endingcredit;
    public float scrollSpeed = 20f;

    private void Start()
    {
        stageGameManager = FindObjectOfType<StageGameManager>();
    }

    void Update()
    {
        endingcredit.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        if (endingcredit.transform.position.y >= 13000f)
        {
            stageGameManager.isending = true;
            stageGameManager.SaveIsEnding(); // isending 값을 저장
            SceneManager.LoadScene("Start Scene");
        }
    }
}
