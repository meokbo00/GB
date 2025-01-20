using GoogleMobileAds.Api;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    private void Start()
    {
        // 광고 SDK 초기화
        MobileAds.Initialize(initStatus => { });
    }
}