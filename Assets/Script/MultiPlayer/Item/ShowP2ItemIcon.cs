using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//획득한 아이템을 아이템 인벤에 저장하는 스크립트
public class ShowP2ItemIcon : MonoBehaviour
{
    public GameObject[] P2IconPlaces;
    public GameObject[] P2Icon;
    private int placedItemCount = 0;
    public AudioSource P2AudioSource;

    public void PrintDestroyedObjectTag(string objecttag)
    {
        GameObject nextIcon = null;

        switch (objecttag)
        {
            case "Item_BlackHole": nextIcon = P2Icon[0]; break;
            case "Item_Durability": nextIcon = P2Icon[1]; break;
            case "Item_Fasten": nextIcon = P2Icon[2]; break;
            case "Item_Force": nextIcon = P2Icon[3]; break;
            case "Item_Invincible": nextIcon = P2Icon[4]; break;
            case "Item_Random_number": nextIcon = P2Icon[5]; break;
            case "Item_Reduction": nextIcon = P2Icon[6]; break;
        }

        if (nextIcon != null)
        {
            foreach (GameObject place in P2IconPlaces)
            {
                if (place.transform.childCount == 0)
                {
                    Instantiate(nextIcon, place.transform.position, Quaternion.identity, place.transform);
                    placedItemCount++;
                    return;
                }
            }
            P2AudioSource = GetComponent<AudioSource>();
            P2AudioSource.Play();
        }
    }
}