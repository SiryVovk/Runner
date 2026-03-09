using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdSystemSinglton : MonoBehaviour
{
    public static AdSystemSinglton Instance { get; private set; }

    public event Action OnRewardGranted;

    private RewardedAd _rewardedAd;

    private bool _rewardEarned;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        MobileAds.Initialize((InitializationStatus initstatus) =>
        {
            if (initstatus == null)
            {
                Debug.LogError("Google Mobile Ads initialization failed.");
                return;
            }

        });

        LoadAd();
    }

    private void LoadAd()
    {
        var adRequest = new AdRequest();

        RewardedAd.Load("AD_UNIT_ID", adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.Log(error);
                return;
            }

            _rewardedAd = ad;
        });
    }

    public void ShowAd()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                _rewardEarned = true;
            });

            _rewardedAd.OnAdFullScreenContentClosed += OnAdClosed;
        }
    }

    private void OnAdClosed()
    {
        if (_rewardEarned)
        {
            OnRewardGranted?.Invoke();
        }

        LoadAd();
    }

    public bool IsAdReady()
    {
        return _rewardedAd != null && _rewardedAd.CanShowAd();
    }
}
