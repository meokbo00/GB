using UnityEngine;

public class StageGameManager : MonoBehaviour
{
    public static StageGameManager instance = null;
    public int StageClearID;
    public float ELlevel;
    public bool isending = false;
    private float ELlevelIDCache;
    private int stageClearIDCache; // PlayerPrefs 값을 캐싱
    private bool isEndingCache;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // PlayerPrefs에서 값 불러오고 캐시에 저장
            ELlevelIDCache = PlayerPrefs.GetFloat("ELlevel", 1);
            stageClearIDCache = PlayerPrefs.GetInt("StageClearID", 0);
            isEndingCache = PlayerPrefs.GetInt("IsEnding", 0) == 1;
            ELlevel = ELlevelIDCache;
            StageClearID = stageClearIDCache;
            isending = isEndingCache;
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f; // 게임 일시정지
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // 게임 일시정지 해제
    }

    public void SaveStageClearID()
    {
        if (stageClearIDCache != StageClearID)  // 값이 변경된 경우에만 저장
        {
            PlayerPrefs.SetInt("StageClearID", StageClearID);
            PlayerPrefs.Save();
            stageClearIDCache = StageClearID;  // 캐시 업데이트
        }
    }
    public void SaveELlevel()
    {
        if (ELlevelIDCache != ELlevel)  // 값이 변경된 경우에만 저장
        {
            PlayerPrefs.SetFloat("ELlevel", ELlevel);
            PlayerPrefs.Save();
            ELlevelIDCache = ELlevel;
        }
    }

    public void SaveIsEnding()
    {
        if (isEndingCache != isending)  // 값이 변경된 경우에만 저장
        {
            PlayerPrefs.SetInt("IsEnding", isending ? 1 : 0);
            PlayerPrefs.Save();
            isEndingCache = isending;  // 캐시 업데이트
        }
    }
}
