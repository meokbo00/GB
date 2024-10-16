using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingleStageStopBtn : MonoBehaviour
{
    public Button Stopbtn;
    public Button Xbtn;
    public Button Resume;
    public Button BacktoCenter;
    public Button Main_menu;

    public GameObject Stop_Channel;
    public AudioSource ButtonAudio;
    StageGameManager StageGameManager;
    bool ispause;
    void Start()
    {
        StageGameManager = FindObjectOfType<StageGameManager>();
        ispause = false;
        this.Stopbtn.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(true);

            Time.timeScale = 0;
            ispause = true;
        });
        this.Xbtn.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;
            return;
        });
        this.Resume.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;
            return;
        });
        this.BacktoCenter.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;

            if (StageGameManager.StageClearID <= 5)
            {
                SceneManager.LoadScene("Stage");
            }
            else if(StageGameManager.StageClearID >= 6)
            {
                SceneManager.LoadScene("Main Stage");
            }
        });
        this.Main_menu.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;
            SceneManager.LoadScene("Start Scene");
        });
    }
}
