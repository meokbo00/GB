using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerSetting : MonoBehaviour
{
    public GameObject SinglePlaySetting;
    public Image FadeIn;
    public Button X;
    public Button NewBtn;
    public Button ContinueBtn;
    public Button ChallengeBtn;
    public Button EndlessBtn;

    public GameObject reallynew;
    public Button reallyYes;
    public Button reallyNo;
    private bool fadeInComplete = false;

    void Start()
    {
        StageGameManager stageGameManager = FindObjectOfType<StageGameManager>();
        this.X.onClick.AddListener(() =>
        {
            SinglePlaySetting.SetActive(false);
        });
        this.NewBtn.onClick.AddListener(() =>
        {
            if (stageGameManager.StageClearID > 1)
            {
                reallynew.SetActive(true);
            }
            else
            {
                ResetStageClearIDAndLoadScene(stageGameManager, "Prologue");
            }
        });
        this.ContinueBtn.onClick.AddListener(() =>
        {
            if(stageGameManager.StageClearID == 1)
            {
                return;
            }
            else if (stageGameManager.StageClearID <= 6.5f && stageGameManager.StageClearID >= 2)
            {
                StartCoroutine(FadeInAndLoadScene("Stage"));
            }
            else if (stageGameManager.StageClearID >= 7)
            {
                StartCoroutine(FadeInAndLoadScene("Main Stage"));
            }
        });
        this.ChallengeBtn.onClick.AddListener(() =>
        {
            StartCoroutine(FadeInAndLoadScene("ChallengeScene"));
        });
        this.EndlessBtn.onClick.AddListener(() =>
        {
            // 여기에 Endless 버튼에 대한 동작을 추가합니다.
        });
        this.reallyNo.onClick.AddListener(() =>
        {
            reallynew.SetActive(false);
        });
        this.reallyYes.onClick.AddListener(() =>
        {
            ResetStageClearIDAndLoadScene(stageGameManager, "Prologue");
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
        StartCoroutine(FadeInAndLoadScene(sceneName));
    }

    IEnumerator FadeInAndLoadScene(string sceneName)
    {
        FadeIn.gameObject.SetActive(true);
        Color originalColor = FadeIn.color;
        while (FadeIn.color.a < 1)
        {
            float newAlpha = FadeIn.color.a + Time.deltaTime / 3;
            FadeIn.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            yield return null;
        }
        fadeInComplete = true;
        if (fadeInComplete)
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(sceneName);
        }
    }
}