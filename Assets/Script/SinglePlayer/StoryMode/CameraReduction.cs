using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class CameraReduction : MonoBehaviour
{

    private Camera mainCamera;
    private int currentIndex = 0;
    private float[] sizes;
    private string[] sizeTexts = { "100%", "75%", "50%", "15%", "1%"};

    public TextMeshProUGUI buttonText; 

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("메인 카메라를 찾을 수 없습니다!");
            return;
        }

        // 현재 씬에 따라 sizes 배열 설정
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Stage")
        {
            sizes = new float[] { 10f, 20f, 30f,50f, 60f};
        }
        else if (currentSceneName == "Main Stage")
        {
            sizes = new float[] { 4f, 7f, 15f,30f, 45f };
        }
        UpdateButtonText(); // 초기 텍스트 업데이트
    }

    public void ChangeCameraSize()
    {
        if (mainCamera != null)
        {
            currentIndex = (currentIndex + 1) % sizes.Length;
            mainCamera.orthographicSize = sizes[currentIndex]; // 카메라 크기 변경
            UpdateButtonText();
        }
    }

    private void UpdateButtonText()
    {
        if (buttonText != null)
        {
            buttonText.text = sizeTexts[currentIndex];
        }
    }
}