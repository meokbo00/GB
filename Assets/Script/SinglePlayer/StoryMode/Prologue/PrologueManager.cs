using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 매니지먼트 네임스페이스 추가
using UnityEngine.UI;

public class PrologueManager : MonoBehaviour
{
    public Image firstImage;
    public Image secondImage;
    public Image FadeIn;

    private int currentChatId;

    void Start()
    {
        StartCoroutine(FadeInAndOut(firstImage));
    }

    IEnumerator FadeInAndOut(Image image)
    {
        for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime * 0.5f)
        {
            SetImageAlpha(image, alpha);
            yield return null;
        }
        SetImageAlpha(image, 1);

        yield return new WaitForSeconds(1);
        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime * 0.5f)
        {
            SetImageAlpha(image, alpha);
            yield return null;
        }
        SetImageAlpha(image, 0);
        image.gameObject.SetActive(false);
        secondImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(4.5f);

        AudioSource audioSource = secondImage.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            TextManager textManager = FindObjectOfType<TextManager>();
            audioSource.Stop();

            ShowText showTextScript = textManager.Linebox.GetComponent<ShowText>();
            if (showTextScript != null)
            {
                showTextScript.OnChatComplete.AddListener(OnChatComplete);
                currentChatId = 1; // 현재 채팅 ID 저장
                textManager.GiveMeTextId(currentChatId);
            }
            else
            {
                Debug.LogError("ShowText script not found on Linebox.");
            }
        }
    }

    void SetImageAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    void OnChatComplete(int chatId)
    {
        Debug.Log($"아이디 {chatId}에 해당하는 문장을 출력완료했습니다.");
        if (chatId == 1)
        {
            StartCoroutine(FadeInImage(FadeIn));
        }
    }

    IEnumerator FadeInImage(Image image)
    {
        image.gameObject.SetActive(true);
        Color color = image.color;
        color.a = 0;
        image.color = color;

        for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime / 3f)
        {
            color.a = alpha;
            image.color = color;
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
