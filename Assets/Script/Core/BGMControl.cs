using UnityEngine.SceneManagement;
using UnityEngine;

public class BGMControl : MonoBehaviour
{
    public AudioSource[] SoundEffect;
    public bool BGMSwitch = true;
    public bool SoundEffectSwitch = true;

    private void Awake()
    {
        // 게임 시작 시 저장된 상태를 불러오기
        LoadAudioSettings();
    }

    void Update()
    {
        UpdateAudioSources();
    }

    void UpdateAudioSources()
    {
        ToggleAudioSources(SoundEffect, SoundEffectSwitch);
    }

    void ToggleAudioSources(AudioSource[] sources, bool isEnabled)
    {
        foreach (var source in sources)
        {
            if (!isEnabled && source.isPlaying)
            {
                source.Stop();
            }
            source.enabled = isEnabled;
        }
    }
    private void StopAllAudio(AudioSource[] sources)
    {
        foreach (var source in sources)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
    }

    // 게임 시작 시 저장된 상태를 불러오기
    private void LoadAudioSettings()
    {
        BGMSwitch = PlayerPrefs.GetInt("BGMSwitch", 1) == 1; // 기본값은 true
        SoundEffectSwitch = PlayerPrefs.GetInt("SoundEffectSwitch", 1) == 1; // 기본값은 true
    }

    // 상태가 변경될 때 저장하기
    public void SaveAudioSettings()
    {
        PlayerPrefs.SetInt("BGMSwitch", BGMSwitch ? 1 : 0);
        PlayerPrefs.SetInt("SoundEffectSwitch", SoundEffectSwitch ? 1 : 0);
        PlayerPrefs.Save();
    }

    // 새로운 메서드 추가
    public void SoundEffectPlay(int index)
    {
        if (index >= 0 && index < SoundEffect.Length)
        {
            SoundEffect[index].Play();
        }
        else
        {
            Debug.LogWarning("SoundEffectPlay: Index out of range.");
        }
    }
}
