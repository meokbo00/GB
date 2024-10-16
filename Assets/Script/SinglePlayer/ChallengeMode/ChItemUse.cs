using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChItemUse : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            string destroyedIconTag = gameObject.tag;
            ChallengeGameManager icontag = FindObjectOfType<ChallengeGameManager>(); // �ӽ÷� ������ �ڵ��Դϴ�.
            icontag.PrintDestroyedicontag(destroyedIconTag);
            Debug.Log("������ " + destroyedIconTag + " �� Ŭ���߽��ϴ�");
            Destroy(gameObject);
        });
    }
}