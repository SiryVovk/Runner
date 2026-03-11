using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdService : MonoBehaviour
{
    public static AdService Instance { get; private set; }

    public event Action OnRewardGranted;

    [SerializeField] private string _adUnitId = "AD_UNIT_ID";

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

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            if (initStatus == null)
            {
                Debug.LogError("Google Mobile Ads initialization failed.");
                return;
            }

        });

        LoadAd();
    }

    private void LoadAd()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        var adRequest = new AdRequest();

        RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
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
        if (IsAdReady())
        {
            _rewardEarned = false;
            _rewardedAd.OnAdFullScreenContentClosed += HandleAdClosed;
            _rewardedAd.Show((Reward reward) =>
            {
                _rewardEarned = true;
            });
        }
    }

    private void HandleAdClosed()
    {
        _rewardedAd.OnAdFullScreenContentClosed -= HandleAdClosed;

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
