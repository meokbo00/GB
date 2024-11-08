using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueManager : MonoBehaviour
{
    public Image firstImage;
    public Image secondImage;
    public Image FadeIn;

    private TextManager textManager;
    private ShowText showTextScript;
    private int currentChatId;

    void Start()
    {
        // TextManager와 ShowText 스크립트를 한 번만 찾고 캐시
        textManager = FindObjectOfType<TextManager>();
        if (textManager != null)
        {
            showTextScript = textManager.Linebox.GetComponent<ShowText>();
        }

        StartCoroutine(FadeInAndOut(firstImage));
    }

    IEnumerator FadeInAndOut(Image image)
    {
        Color color = image.color;

        // 페이드 인
        for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime * 0.5f)
        {
            color.a = alpha;
            image.color = color;
            yield return null;
        }
        color.a = 1;
        image.color = color;

        yield return new WaitForSeconds(1);

        // 페이드 아웃
        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime * 0.5f)
        {
            color.a = alpha;
            image.color = color;
            yield return null;
        }
        color.a = 0;
        image.color = color;
        image.gameObject.SetActive(false);
        secondImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(4.5f);

        // 두 번째 이미지에 대한 오디오 및 텍스트 처리
        AudioSource audioSource = secondImage.GetComponent<AudioSource>();
        if (audioSource != null && showTextScript != null)
        {
            audioSource.Stop();

            // OnChatComplete 이벤트 리스너 등록
            showTextScript.OnChatComplete.AddListener(OnChatComplete);
            currentChatId = 1; // 현재 채팅 ID 설정
            textManager.GiveMeTextId(currentChatId);
        }
    }

    void OnChatComplete(int chatId)
    {
        Debug.Log($"아이디 {chatId}에 해당하는 문장을 출력완료했습니다.");
        if (chatId == 1)
        {
            StartCoroutine(FadeInImage(FadeIn));

            // 이벤트 리스너 해제하여 메모리 관리 및 최적화
            showTextScript.OnChatComplete.RemoveListener(OnChatComplete);
        }
    }

    IEnumerator FadeInImage(Image image)
    {
        image.gameObject.SetActive(true);
        Color color = image.color;
        color.a = 0;
        image.color = color;

        float fadeDuration = 3f;
        float timeElapsed = 0f;

        // 페이드 인
        while (timeElapsed < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
            image.color = color;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        image.color = color;

        // 알파값이 다 올라간 후 2.5초 대기
        yield return new WaitForSeconds(2.5f);

        // "Prologue1.5" 씬으로 전환
        SceneManager.LoadScene("Prologue1.5");
    }
}
