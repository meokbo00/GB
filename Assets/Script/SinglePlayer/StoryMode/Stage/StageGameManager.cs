using UnityEngine;

public class StageGameManager : MonoBehaviour
{
    public static StageGameManager instance = null;
    public int StageClearID;


    public int ELRound;
    public int ELnum;
    public float ELlevel;
    private int ELnumIDCache;
    private float ELlevelIDCache;
    private int ELRoundIDCache;

    public bool isending = false;
    private int stageClearIDCache; // PlayerPrefs 값을 캐싱
    private bool isEndingCache;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            ELRoundIDCache = PlayerPrefs.GetInt("ELRound", 1);
            ELnumIDCache = PlayerPrefs.GetInt("ELnum", 1);
            ELlevelIDCache = PlayerPrefs.GetFloat("ELlevel", 2);
            stageClearIDCache = PlayerPrefs.GetInt("StageClearID", 0);
            isEndingCache = PlayerPrefs.GetInt("IsEnding", 0) == 1;

            ELRound = ELRoundIDCache;
            ELnum = ELnumIDCache;
            ELlevel = ELlevelIDCache;
            StageClearID = stageClearIDCache;
            isending = isEndingCache;
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }


    //초기화용 메서드

    //private void Start()
    //{
    //    ELnum = 1;
    //    ELlevel = 2;
    //    ELRound = 1;
    //    SaveELlevelAndELnum();
    //    StageClearID = 0;
    //    isending = false;
    //    SaveStageClearID();
    //    SaveIsEnding();
    //}


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
    public void SaveELlevelAndELnum ()
    {
        if (ELRoundIDCache != ELRound)
        {
            PlayerPrefs.SetInt("ELRound", ELRound);
            PlayerPrefs.Save();
            ELRoundIDCache = ELRound;
        }
        if (ELnumIDCache != ELnum)
        {
            PlayerPrefs.SetInt("ELnum", ELnum);
            PlayerPrefs.Save();
            ELnumIDCache = ELnum;
        }
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
