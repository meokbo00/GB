using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//획득한 아이템을 아이템 인벤에 저장하는 스크립트

public class ShowP1ItemIcon : MonoBehaviour
{
    public GameObject[] P1IconPlaces;
    public GameObject[] P1Icon;
    public AudioSource fullItemAudio;

    public void PrintDestroyedObjectTag(string objecttag)
    {
        GameObject nextIcon = null;

        switch (objecttag)
        {
            case "Item_Big": nextIcon = P1Icon[0]; break;
            case "Item_Small":nextIcon = P1Icon[1];break;
            case "Item_Twice":nextIcon = P1Icon[2];break;
            case "Item_Endless":nextIcon = P1Icon[3];break;
            case "Item_Invincible": nextIcon = P1Icon[4];break;
        }
        if (nextIcon != null)
        {
            foreach (GameObject place in P1IconPlaces)
            {
                if (place.transform.childCount == 0)
                {
                    Instantiate(nextIcon, place.transform.position, Quaternion.identity, place.transform);
                    return;
                }
            }
            fullItemAudio = GetComponent<AudioSource>();
            fullItemAudio.Play();
        }
    }
}