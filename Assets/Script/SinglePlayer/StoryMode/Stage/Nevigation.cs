using UnityEngine;

public class Navigation : MonoBehaviour
{
    private GameObject clearhereObject;
    private GameObject mainPlayerObject;

    public GameObject Top;
    public GameObject Bottom;
    public GameObject Left;
    public GameObject Right;

    private Vector3 lastClearherePosition;

    void Start()
    {
        mainPlayerObject = GameObject.Find("Main Player");

        // Clearhere가 이미 존재한다면 찾기
        clearhereObject = GameObject.Find("Clearhere(Clone)");
        if (clearhereObject != null)
        {
            lastClearherePosition = clearhereObject.transform.position;
            UpdateActivation();
        }
    }

    void FixedUpdate()
    {
        // Clearhere 오브젝트를 찾지 못했을 경우 한 번만 찾기 시도
        if (clearhereObject == null)
        {
            clearhereObject = GameObject.Find("Clearhere(Clone)");
            if (clearhereObject != null)
            {
                lastClearherePosition = clearhereObject.transform.position;
                UpdateActivation();
            }
        }
        else
        {
            // 위치가 변경되었을 때만 업데이트
            Vector3 currentClearherePosition = clearhereObject.transform.position;
            if (currentClearherePosition != lastClearherePosition)
            {
                lastClearherePosition = currentClearherePosition;
                UpdateActivation();
            }
        }
    }

    void UpdateActivation()
    {
        Vector3 mainPlayerPosition = mainPlayerObject.transform.position;
        Vector3 clearherePosition = clearhereObject.transform.position;

        bool isTop = clearherePosition.y > mainPlayerPosition.y;
        bool isBottom = clearherePosition.y <= mainPlayerPosition.y;
        bool isLeft = clearherePosition.x < mainPlayerPosition.x;
        bool isRight = clearherePosition.x >= mainPlayerPosition.x;

        Top.SetActive(isTop);
        Bottom.SetActive(isBottom);
        Left.SetActive(isLeft);
        Right.SetActive(isRight);
    }
}
