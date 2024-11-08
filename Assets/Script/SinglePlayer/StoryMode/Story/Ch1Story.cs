using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ch1Story : MonoBehaviour
{
    public GameObject Clearhere;
    public Camera mainCamera;
    public Image FadeIn;
    public GameObject Stage;
    public GameObject RemainTime;

    private ShowText showText;
    private StageGameManager stageGameManager;
    private StageBallController stageBallController;
    private ContinuousRandomMovement[] randomMovements;

    void Start()
    {
        // `FindAnyObjectByType`로 필요한 객체를 한 번만 찾고 캐시
        stageGameManager = FindAnyObjectByType<StageGameManager>();
        TextManager textManager = FindAnyObjectByType<TextManager>();
        stageBallController = FindAnyObjectByType<StageBallController>();
        randomMovements = FindObjectsOfType<ContinuousRandomMovement>();

        // 초기 조건에 따라 필요한 작업을 실행하는 코루틴을 시작
        switch (stageGameManager.StageClearID)
        {
            case 1:
                stageBallController.enabled = false;
                textManager.GiveMeTextId(1);
                showText = FindAnyObjectByType<ShowText>();

                StartCoroutine(HandleStage1());
                break;
            case 2:
                textManager.GiveMeTextId(2);
                break;
            case 5.5f:
                Destroy(Stage);
                textManager.GiveMeTextId(3);
                showText = FindAnyObjectByType<ShowText>();

                StartCoroutine(HandleStage5_5());
                break;
            case 6:
                Destroy(Stage);
                RemainTime.SetActive(true);
                break;
        }
    }

    private IEnumerator HandleStage1()
    {
        // stageClearID가 1일 때 필요한 로직
        while (showText.logTextIndex < 17)
        {
            yield return null;
        }

        stageBallController.enabled = true;

        // 이후의 조건 체크 및 동작
        while (showText.logTextIndex < 41)
        {
            Stage.SetActive(false);
            yield return null;
        }

        Stage.SetActive(true);
        Clearhere.SetActive(true);
    }

    private IEnumerator HandleStage5_5()
    {
        // 특정 logTextIndex가 될 때까지 대기 후 카메라 사이즈 변경
        while (showText.logTextIndex < 4)
        {
            yield return null;
        }

        StartCoroutine(IncreaseCameraSize(mainCamera, 112, 5.5f));

        // 추가 조건에 따른 동작 실행
        while (showText.logTextIndex < 8)
        {
            yield return null;
        }

        ToggleRandomMovement(false);

        while (showText.logTextIndex < 23)
        {
            yield return null;
        }

        ToggleRandomMovement(true);
        StartCoroutine(HandleCameraAndFadeIn(mainCamera, 15, 7f));
    }

    private void ToggleRandomMovement(bool isEnabled)
    {
        foreach (ContinuousRandomMovement randomMovement in randomMovements)
        {
            randomMovement.enabled = isEnabled;
        }
    }

    IEnumerator IncreaseCameraSize(Camera camera, float targetSize, float duration)
    {
        float startSize = camera.orthographicSize;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            camera.orthographicSize = Mathf.Lerp(startSize, targetSize, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        camera.orthographicSize = targetSize;
    }

    IEnumerator HandleCameraAndFadeIn(Camera camera, float targetSize, float duration)
    {
        yield return StartCoroutine(IncreaseCameraSize(camera, targetSize, duration));
        RemainTime.SetActive(true);
        yield return new WaitForSeconds(15f);

        FadeIn.gameObject.SetActive(true);
        Color fadeColor = FadeIn.color;
        float fadeDuration = 2f;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            fadeColor.a = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
            FadeIn.color = fadeColor;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeColor.a = 1;
        FadeIn.color = fadeColor;
        yield return new WaitForSeconds(3f);

        stageGameManager.StageClearID += 0.5f;
        stageGameManager.SaveStageClearID();
        SceneManager.LoadScene("Prologue 2");
    }
}
