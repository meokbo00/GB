using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
using TMPro; // TextMeshPro 네임스페이스 추가
=======
using TMPro;
>>>>>>> Stashed changes

public class TranslateUI : MonoBehaviour
{
    private TMP_Text[] allTexts;
    private StageGameManager stageGameManager;

    // 번역 딕셔너리: 원본 텍스트 -> 영어 번역
    private Dictionary<string, string> translationDictionary = new Dictionary<string, string>()
    {
        { "크레딧", "Credit" },
<<<<<<< Updated upstream
        { "돌아가기", "Back to Menu" },
=======
        { "돌아가기", "Back" },
>>>>>>> Stashed changes
        { "스토리모드", "Story Mode" },
        { "연습모드", "Practice Mode" },
        { "무한모드", "Endless Mode" },
        { "뒤로가기", "Back" },
        { "새 게임", "New" },
        { "이어하기", "Con\ntinue" },
        { "기존 저장정보를 덮어씁니다.\r\n계속하시겠습니까?", "This will overwrite your existing save." },
        { "스토리모드를 먼저 플레이 후\r\n즐기시는 것을 추천 드립니다.", "Try story mode first\r\nfor the best experience" },
        { "계속하기", "Resume" },
        { "재시작", "Retry" },
        { "스테이지 맵", "Stage Map" },
        { "메인 메뉴", "Main Menu" },
        { "일시 정지", "Pause" },
<<<<<<< Updated upstream
        // 필요한 만큼 추가
    };
    private void Update()
    {
        // StageGameManager 객체 찾기
=======
        { "랜덤 스테이지", "Random Stage" },
        { "재도전", "Retry" },
        { "포기", "Give up" },
        { "당신의 점수는", "Your Score is" },
        { "위치 초기화", "Reset location" }
        // 필요한 만큼 추가
    };

    // 영어 -> 한국어 딕셔너리 생성
    private Dictionary<string, string> reverseTranslationDictionary;

    private void Start()
    {
        // 역번역 딕셔너리 생성
        reverseTranslationDictionary = new Dictionary<string, string>();
        foreach (var kvp in translationDictionary)
        {
            reverseTranslationDictionary[kvp.Value] = kvp.Key;
        }
    }

    private void Update()
    {
>>>>>>> Stashed changes
        stageGameManager = FindAnyObjectByType<StageGameManager>();

        // 현재 씬의 모든 TextMeshPro 객체 가져오기
        allTexts = FindObjectsOfType<TMP_Text>();

<<<<<<< Updated upstream
        // 영어로 변환 조건 확인
        if (stageGameManager.isenglish)
        {
            TranslateAllTextsToEnglish();
        }
    }

    void TranslateAllTextsToEnglish()
    {
        foreach (var text in allTexts)
        {
            // 텍스트 정규화
            string normalizedText = text.text.Replace("\r\n", "\n").Trim();

            // 딕셔너리의 키도 정규화하여 비교
            foreach (var kvp in translationDictionary)
            {
                string normalizedKey = kvp.Key.Replace("\r\n", "\n").Trim();

                if (normalizedKey == normalizedText)
                {
                    text.text = kvp.Value; // 영어로 변환
                    break;
=======
        // 번역 조건 확인
        if (stageGameManager.isenglish)
        {
            TranslateSpecificPartsToEnglish();
        }
        else
        {
            TranslateSpecificPartsToKorean();
        }
    }

    void TranslateSpecificPartsToEnglish()
    {
        foreach (var text in allTexts)
        {
            string originalText = text.text;

            foreach (var kvp in translationDictionary)
            {
                if (originalText.Contains(kvp.Key))
                {
                    // 해당 부분만 번역
                    text.text = originalText.Replace(kvp.Key, kvp.Value);
>>>>>>> Stashed changes
                }
            }
        }
    }

<<<<<<< Updated upstream
=======
    void TranslateSpecificPartsToKorean()
    {
        foreach (var text in allTexts)
        {
            string originalText = text.text;

            foreach (var kvp in reverseTranslationDictionary)
            {
                if (originalText.Contains(kvp.Key))
                {
                    // 해당 부분만 번역
                    text.text = originalText.Replace(kvp.Key, kvp.Value);
                }
            }
        }
    }
>>>>>>> Stashed changes
}
