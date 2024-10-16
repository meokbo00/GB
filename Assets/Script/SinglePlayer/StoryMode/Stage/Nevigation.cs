using UnityEngine;

public class Navigation : MonoBehaviour
{
    private GameObject clearhereObject;
    private GameObject mainPlayerObject;

    public GameObject Top;
    public GameObject Bottom;
    public GameObject Left;
    public GameObject Right;

    void Start()
    {
        mainPlayerObject = GameObject.Find("Main Player");
        if (mainPlayerObject == null)
        {
            Debug.LogError("Main Player 오브젝트를 찾을 수 없습니다.");
        }
    }

    void Update()
    {
        if (clearhereObject == null)
        {
            clearhereObject = GameObject.Find("Clearhere(Clone)");
        }

        if (clearhereObject != null)
        {
            UpdateActivation();
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