using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Rigidbody2D rigid;
    public int ShieldHP;
    public int initialRandomNumber; // 초기 randomNumber 값을 저장할 변수
    public TextMeshPro textMesh;
    public bool isShowHP;
    public bool isHide;

    public int MaxHP;
    public int MinHP;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        ShieldHP = Random.Range(MinHP, MaxHP);
        initialRandomNumber = ShieldHP; // 초기 randomNumber 값을 저장
        if (isShowHP)
        {
            textMesh.text = ShieldHP.ToString();
        }
        textMesh.fontSize = 3;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero;
        textMesh.sortingOrder = 3;


        if (isHide)
        {
            ChangeObjectColor(transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "P1ball" || coll.gameObject.tag == "P2ball" || coll.gameObject.tag == "P1Item" || coll.gameObject.tag == "P2Item"
            || (coll.gameObject.tag == "Item" && coll.gameObject.name != "SPEndlessF(Clone)"))
        {
            if (ShieldHP > 0)
            {
                ShieldHP--;
                if (isShowHP)
                {
                    textMesh.text = ShieldHP.ToString();
                }
            }
            if (ShieldHP <= 0)
            {
                Destroy(gameObject); // 부모 오브젝트 삭제
            }
        }
        if (coll.gameObject.name == "SPTwiceF(Clone)")
        {
            ShieldHP -= 1;
            if (ShieldHP > 0)
            {
                textMesh.text = ShieldHP.ToString();
            }
            if (ShieldHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ChangeObjectColor(Transform parentTransform)
    {
        if (!isHide)
            return;

        // 자기 자신의 색상 변경
        SpriteRenderer currentSpriteRenderer = parentTransform.GetComponent<SpriteRenderer>();
        if (currentSpriteRenderer != null)
        {
            GameObject background = GameObject.Find("BackGround");
            if (background != null)
            {
                SpriteRenderer backgroundSpriteRenderer = background.GetComponent<SpriteRenderer>();
                if (backgroundSpriteRenderer != null)
                {
                    currentSpriteRenderer.color = backgroundSpriteRenderer.color;
                }
            }
        }

        // 부모 오브젝트의 자식들을 검사하면서 색상을 변경
        foreach (Transform child in parentTransform)
        {
            ChangeObjectColor(child);
        }
    }
}
