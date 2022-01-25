using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
    [SerializeField] BannerPosition bannerPosition;

    [SerializeField] string androidAdUnitId = "Banner_Android";

    private void Start()
    {
        Advertisement.Banner.SetPosition(bannerPosition);
        LoadBanner();
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(androidAdUnitId, options);
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner error: {message}");
    }

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(androidAdUnitId, options);
    }

    private void OnBannerClicked()
    {
    }

    private void OnBannerHidden()
    {
    }

    private void OnBannerShown()
    {
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
}
