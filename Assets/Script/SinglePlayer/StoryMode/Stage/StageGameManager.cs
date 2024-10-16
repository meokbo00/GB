using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageGameManager : MonoBehaviour
{
    public static StageGameManager instance = null;
    public float StageClearID;
    public bool isending = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadStageClearID();
            LoadIsEnding();
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
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
        PlayerPrefs.SetFloat("StageClearID", StageClearID);
        PlayerPrefs.Save();
    }

    public void SaveIsEnding()
    {
        PlayerPrefs.SetInt("IsEnding", isending ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadStageClearID()
    {
        if (PlayerPrefs.HasKey("StageClearID"))
        {
            StageClearID = PlayerPrefs.GetFloat("StageClearID");
        }
    }

    private void LoadIsEnding()
    {
        if (PlayerPrefs.HasKey("IsEnding"))
        {
            isending = PlayerPrefs.GetInt("IsEnding") == 1;
        }
    }
}
