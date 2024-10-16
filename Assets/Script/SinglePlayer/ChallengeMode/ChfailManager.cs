using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ChallengeGameManager;

public class ChfailManager : MonoBehaviour
{
    public Button retry;
    public Button back;
    public TMP_Text scoretext;
    void Start()
    {
        this.retry.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("ChallengeScene");
        });
        this.back.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Start Scene");
        });

        int maxScore = PlayerPrefs.GetInt("MaxScore", 0); // 저장된 스코어 가져오기
        scoretext.text = "Best Score : " + maxScore +  "\nScore : " + GameData.CurrentScore.ToString();
    }

    void Update()
    {
        
    }
}
