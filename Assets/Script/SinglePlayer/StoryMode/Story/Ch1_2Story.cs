using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �߰�

public class Ch1_2Story : MonoBehaviour
{
    public AudioSource BGM;
    StageGameManager stageGameManager;
    StageBallController stageBallController;
    private ShowText showText;
    public GameObject Clock;
    public GameObject Fadeinout;
    public GameObject creation;
    RemainTime remainTime;
    public Image fadeImage;
    public GameObject isplaybgm;

    void Start()
    {
        BGM.Stop();
        showText = FindObjectOfType<ShowText>();
        fadeImage = Fadeinout.GetComponent<Image>();
        stageGameManager = FindObjectOfType<StageGameManager>();
        TextManager textManager = FindObjectOfType<TextManager>();
        stageBallController = FindObjectOfType<StageBallController>();

        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        if (stageGameManager.StageClearID == 6f)
        {
            stageBallController.enabled = false;
            Fadeinout.SetActive(true);
            textManager.GiveMeTextId(4);
            Clock.SetActive(true);
            remainTime = FindObjectOfType<RemainTime>();
            remainTime.years = 0;
            remainTime.days = 0;
            remainTime.hours = 0;
            remainTime.minutes = 0;
            remainTime.seconds = 0;

            SpriteRenderer[] spriteRenderers = creation.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.color = Color.black;
            }

            ContinuousRandomMovement[] randomMovements = creation.GetComponentsInChildren<ContinuousRandomMovement>();
            foreach (ContinuousRandomMovement rm in randomMovements)
            {
                rm.enabled = false;
            }
        }
    }

    void Update()
    {
        showText = FindObjectOfType<ShowText>();

        if (showText != null && stageGameManager.StageClearID == 6f)
        {
            if (showText.logTextIndex > 38)
            {
                stageBallController.enabled = true;
                Fadeinout.SetActive(false);
                isplaybgm.SetActive(false);
            }
            if (showText.logTextIndex == 48)
            {
                StartCoroutine(FadeIn());
            }
        }
    }
    IEnumerator FadeIn()
    {
        Fadeinout.SetActive(true);
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        while (color.a < 1f)
        {
            color.a += Time.deltaTime / 5; // 2�ʿ� ���� ���̵� ��
            fadeImage.color = color;
            yield return null;
        }

        // ���İ��� 1�� �Ǹ� 3�� ���
        yield return new WaitForSeconds(3f);

        // "Main Stage"�� �� ��ȯ
        SceneManager.LoadScene("Main Stage");
    }
}
