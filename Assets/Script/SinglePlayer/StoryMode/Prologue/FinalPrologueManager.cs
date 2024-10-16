using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UnityEngine.UI를 사용하여 Button을 포함합니다.
using TMPro; // TextMeshPro를 포함합니다.

public class FinalPrologueManager : MonoBehaviour
{
    public GameObject[] images;
    public Button transitionButton; // TMP 버튼은 일반 Button과 함께 사용됩니다.

    private Coroutine deactivateButtonCoroutine;

    void Start()
    {
        foreach (GameObject image in images)
        {
            CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = image.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0f;
        }

        transitionButton.gameObject.SetActive(false); // 시작할 때 버튼 비활성화

        StartCoroutine(ShowImagesSequentially());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 클릭 시
        {
            ActivateButton();
        }
    }

    IEnumerator ShowImagesSequentially()
    {
        float waitTime = 4.5f;

        foreach (GameObject image in images)
        {
            yield return StartCoroutine(FadeIn(image));
            yield return new WaitForSeconds(waitTime); // 대기 시간 설정
            yield return StartCoroutine(FadeOut(image));
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Stage");
    }

    IEnumerator FadeIn(GameObject image)
    {
        CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
        float duration = 2f;
        float elapsed = 0f;

        image.SetActive(true);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOut(GameObject image)
    {
        CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1f - (elapsed / duration));
            yield return null;
        }
        canvasGroup.alpha = 0f;

        image.SetActive(false);
    }

    void ActivateButton()
    {
        transitionButton.gameObject.SetActive(true); // 버튼 활성화
        transitionButton.onClick.AddListener(OnButtonClick); // 버튼 클릭 시 이벤트 추가

        if (deactivateButtonCoroutine != null)
        {
            StopCoroutine(deactivateButtonCoroutine);
        }
        deactivateButtonCoroutine = StartCoroutine(DeactivateButtonAfterDelay());
    }

    IEnumerator DeactivateButtonAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        if (transitionButton.gameObject.activeSelf)
        {
            transitionButton.gameObject.SetActive(false); // 버튼 비활성화
        }
    }

    void OnButtonClick()
    {
        SceneManager.LoadScene("Stage"); // "Stage" 씬으로 전환
    }
}
