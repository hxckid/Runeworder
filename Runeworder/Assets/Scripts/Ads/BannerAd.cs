using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
    [SerializeField] BannerPosition bannerPosition;

    [SerializeField] string androidAdUnitId = "Banner_Android";
    [SerializeField] float bannerLoadDelay = 2f;

    private void Start()
    {
        Advertisement.Banner.SetPosition(bannerPosition);
        StartCoroutine(LoadBannerAfterTime(bannerLoadDelay));
        //LoadBanner();
    }

    private IEnumerator LoadBannerAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
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

        Debug.Log("Banner Show");
        Advertisement.Banner.Show(androidAdUnitId, options);
    }

    private void OnBannerClicked()
    {
        Debug.Log("Banner Clicked");
    }

    private void OnBannerHidden()
    {
        Debug.Log("Banner Hidden");
    }

    private void OnBannerShown()
    {
        Debug.Log("Banner Shown");
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
}
