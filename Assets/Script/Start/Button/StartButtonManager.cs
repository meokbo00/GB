using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartButtonManager : MonoBehaviour
{
    public GameObject p1orp2;
    public GameObject singleSetting;
    public Button Play;
    public Button Setting;
    public Button Quit;

    public Button singleP;
    public Button multiP;
    public Button x;


    public AudioSource ButtonAudio;

    void Start()
    {
        this.Play.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            p1orp2.gameObject.SetActive(true);
        });

        this.Setting.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            SceneManager.LoadScene("Setting Scene");
        });

        this.Quit.onClick.AddListener(() =>
        {
            Debug.Log("게임 종료!");
            ButtonAudio.Play();
            Application.Quit();
        });

        this.singleP.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            singleSetting.gameObject.SetActive(true);
        });

        this.multiP.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            SceneManager.LoadScene("Main Scene");
        });

        this.x.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.p1orp2.gameObject.SetActive(false);
        });
    }
}