using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SinglePlayerSetting : MonoBehaviour
{
    public GameObject SinglePlaySetting;
    public CanvasGroup fadeCanvasGroup; // CanvasGroup 추가
    public Button X;
    public Button NewBtn;
    public Button ContinueBtn;
    public Button ChallengeBtn;
    public Button EndlessBtn;

    public GameObject reallynew;
    public Button reallyYes;
    public Button reallyNo;

    void Start()
    {
        StageGameManager stageGameManager = FindObjectOfType<StageGameManager>();
        X.onClick.AddListener(() =>
        {
            SinglePlaySetting.SetActive(false);
        });
        NewBtn.onClick.AddListener(() =>
        {
            if (stageGameManager.StageClearID > 1)
            {
                reallynew.SetActive(true);
            }
            else
            {
                ResetStageClearIDAndLoadScene(stageGameManager, "Prologue1.5");
            }
        });
        ContinueBtn.onClick.AddListener(() =>
        {
            if (stageGameManager.StageClearID == 1)
            {
                return;
            }
            else if (stageGameManager.StageClearID <= 6.5f && stageGameManager.StageClearID >= 2)
            {
                StartFadeIn("Stage");
            }
            else if (stageGameManager.StageClearID >= 7)
            {
                StartFadeIn("Main Stage");
            }
        });
        ChallengeBtn.onClick.AddListener(() =>
        {
            StartFadeIn("ChallengeScene");
        });
        EndlessBtn.onClick.AddListener(() =>
        {
            StartFadeIn("EndlessInGame");
        });
        reallyNo.onClick.AddListener(() =>
        {
            reallynew.SetActive(false);
        });
        reallyYes.onClick.AddListener(() =>
        {
            ResetStageClearIDAndLoadScene(stageGameManager, "Prologue1.5");
        });
    }

    void ResetStageClearIDAndLoadScene(StageGameManager stageGameManager, string sceneName)
    {
        stageGameManager.StageClearID = 1;
        stageGameManager.isending = false;
        PlayerPrefs.SetFloat("StageClearID", stageGameManager.StageClearID);
        PlayerPrefs.SetInt("isending", stageGameManager.isending ? 1 : 0);
        stageGameManager.SaveIsEnding();
        stageGameManager.SaveStageClearID();
        StartFadeIn(sceneName);
    }

    void StartFadeIn(string sceneName)
    {
        fadeCanvasGroup.alpha = 0; // 초기 알파값을 0으로 설정
        fadeCanvasGroup.gameObject.SetActive(true); // CanvasGroup 활성화
        fadeCanvasGroup.DOFade(1, 3f) // 3초 동안 알파값을 1로 애니메이션
            .SetUpdate(true) // TimeScale 영향을 받지 않도록 설정
            .OnComplete(() =>
            {
                SceneManager.LoadScene(sceneName); // 페이드 인 완료 후 씬 로드
            });
    }
}
