using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �߰�

public class Ch3Story : MonoBehaviour
{
    public Camera Camera;
    public GameObject Fadein;
    public AudioSource[] AudioSources; // ����� �ҽ� �迭
    StageGameManager stageGameManager;
    TextManager textManager;
    ShowText showText;
    private bool isZooming = false;

    private bool isAudio1Played = false; // ù ��° ����� ��� ����
    private bool isAudio2Played = false; // �� ��° ����� ��� ����

    void Start()
    {
        showText = FindObjectOfType<ShowText>();
        textManager = FindObjectOfType<TextManager>();
        stageGameManager = FindObjectOfType<StageGameManager>();

        if (stageGameManager.StageClearID == 65)
        {
            textManager.GiveMeTextId(1);
        }
    }

    void Update()
    {
        showText = FindObjectOfType<ShowText>();
        if (showText != null && stageGameManager.StageClearID == 65)
        {
            if (showText.logTextIndex == 8 && !isZooming)
            {
                StartCoroutine(SmoothZoom(5f, 1700f));
            }
            if (showText.logTextIndex == 12)
            {
                StartCoroutine(ExecuteAfterDelay(4f));
            }
            if (showText.logTextIndex == 28)
            {
                StartCoroutine(LoadSceneAfterDelay(4f, "Story-InGame"));
            }
        }
    }

    IEnumerator SmoothZoom(float duration, float targetSize)
    {
        isZooming = true;
        float startSize = Camera.orthographicSize;
        float elapsed = 0f;

        // ����� �ҽ� �迭�� ù ��° ������� ������� ���� ��� ���
        if (AudioSources.Length > 0 && !isAudio1Played)
        {
            AudioSources[0].Play();
            isAudio1Played = true; // ù ��° ����� ��� ���� ����
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Camera.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsed / duration);
            yield return null;
        }

        Camera.orthographicSize = targetSize;
        isZooming = false;
    }

    IEnumerator ExecuteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Fadein.SetActive(true);

        // ����� �ҽ� �迭�� �� ��° ������� ������� ���� ��� ���
        if (AudioSources.Length > 1 && !isAudio2Played)
        {
            AudioSources[1].Play();
            isAudio2Played = true; // �� ��° ����� ��� ���� ����
        }
    }

    IEnumerator LoadSceneAfterDelay(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
