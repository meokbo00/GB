using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가

public class TranslateUI : MonoBehaviour
{
    private TMP_Text[] allTexts;
    private StageGameManager stageGameManager;

    // 번역 딕셔너리: 원본 텍스트 -> 영어 번역
    private Dictionary<string, string> translationDictionary = new Dictionary<string, string>()
    {
        { "크레딧", "Credit" },
        { "돌아가기", "Back to Menu" },
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
        // 필요한 만큼 추가
    };
    private void Update()
    {
        // StageGameManager 객체 찾기
        stageGameManager = FindAnyObjectByType<StageGameManager>();

        // 현재 씬의 모든 TextMeshPro 객체 가져오기
        allTexts = FindObjectsOfType<TMP_Text>();

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
                }
            }
        }
    }

}
