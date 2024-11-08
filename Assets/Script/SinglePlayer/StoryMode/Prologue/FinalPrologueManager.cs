using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UnityEngine.UI를 사용하여 Button을 포함합니다.

public class FinalPrologueManager : MonoBehaviour
{
    public GameObject[] images;
    public Button transitionButton; // TMP 버튼은 일반 Button과 함께 사용됩니다.

    private Coroutine deactivateButtonCoroutine;
    private CanvasGroup[] canvasGroups;

    void Start()
    {
        // CanvasGroup을 캐시합니다.
        canvasGroups = new CanvasGroup[images.Length];
        for (int i = 0; i < images.Length; i++)
        {
            canvasGroups[i] = images[i].GetComponent<CanvasGroup>() ?? images[i].AddComponent<CanvasGroup>();
            canvasGroups[i].alpha = 0f;
        }

        transitionButton.gameObject.SetActive(false); // 시작할 때 버튼 비활성화
        transitionButton.onClick.AddListener(OnButtonClick); // 버튼 클릭 시 이벤트 추가
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
            yield return StartCoroutine(Fade(image, true, 2f)); // 페이드 인
            yield return new WaitForSeconds(waitTime); // 대기 시간 설정
            yield return StartCoroutine(Fade(image, false, 1f)); // 페이드 아웃
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Stage");
    }

    IEnumerator Fade(GameObject image, bool fadeIn, float duration)
    {
        CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
        image.SetActive(true);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(fadeIn ? (elapsed / duration) : (1f - (elapsed / duration)));
            yield return null;
        }

        canvasGroup.alpha = fadeIn ? 1f : 0f; // 최종 alpha 값 설정

        if (!fadeIn)
        {
            image.SetActive(false); // 페이드 아웃 후 비활성화
        }
    }

    void ActivateButton()
    {
        if (!transitionButton.gameObject.activeSelf) // 이미 활성화되어 있는지 확인
        {
            transitionButton.gameObject.SetActive(true); // 버튼 활성화
            if (deactivateButtonCoroutine != null)
            {
                StopCoroutine(deactivateButtonCoroutine);
            }
            deactivateButtonCoroutine = StartCoroutine(DeactivateButtonAfterDelay());
        }
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
