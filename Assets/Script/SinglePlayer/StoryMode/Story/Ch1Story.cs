using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ch1Story : MonoBehaviour
{
    public GameObject Clearhere;
    private ShowText showText;
    private StageGameManager stageGameManager;
    public Camera mainCamera;
    public Image FadeIn;
    public GameObject Stage;
    public GameObject RemainTime;
    StageBallController stageBallController;

    void Start()
    {
        stageGameManager = FindObjectOfType<StageGameManager>();
        TextManager textManager = FindObjectOfType<TextManager>();
        stageBallController = FindObjectOfType<StageBallController>();
        showText = FindObjectOfType<ShowText>();
        if (showText != null)
        {
            showText.OnChatComplete.AddListener(OnChatCompleteHandler);
        }

        switch (stageGameManager.StageClearID)
        {
            case 1:
                stageBallController.enabled = false;
                textManager.GiveMeTextId(1);
                break;
            case 2:
                textManager.GiveMeTextId(2);
                break;
            case 5.5f:
                Destroy(Stage);
                textManager.GiveMeTextId(3);
                break;
            case 6:
                Destroy(Stage);
                RemainTime.SetActive(true);
                break;
        }
    }

    void Update()
    {
        showText = FindObjectOfType<ShowText>();

        if (showText != null && stageGameManager.StageClearID == 1)
        {
            if (showText.logTextIndex == 17)
            {
                stageBallController.enabled = true;
            }
            if (showText.logTextIndex < 41)
            {
                Stage.SetActive(false);
            }
            if (showText.logTextIndex >= 42)
            {
                Stage.SetActive(true);
                Clearhere.gameObject.SetActive(true);
            }
        }

        if (showText != null && stageGameManager.StageClearID == 5.5)
        {
            if (showText.logTextIndex == 4)
            {
                StartCoroutine(IncreaseCameraSize(mainCamera, 112, 5.5f));
            }
            if (showText.logTextIndex == 8)
            {
                ContinuousRandomMovement[] randomMovements = FindObjectsOfType<ContinuousRandomMovement>();
                foreach (ContinuousRandomMovement randomMovement in randomMovements)
                {
                    randomMovement.enabled = false;
                }
            }
            if (showText.logTextIndex == 23)
            {
                ContinuousRandomMovement[] randomMovements = FindObjectsOfType<ContinuousRandomMovement>();
                foreach (ContinuousRandomMovement randomMovement in randomMovements)
                {
                    randomMovement.enabled = true;
                }
                StartCoroutine(HandleCameraAndFadeIn(mainCamera, 15, 7f));
            }
        }
    }

    private void OnChatCompleteHandler(int chatId)
    {
        if (chatId == 1)
        {
            Debug.Log("ID 1 채팅이 완료되었습니다.");
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
        float fadeDuration = 2f; // 페이드 인 시간 설정
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