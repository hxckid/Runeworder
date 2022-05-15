using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string gameId;
    [SerializeField] bool testMode;

    private void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        Advertisement.Initialize(gameId, testMode); // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< CHange FALSE FOR testMode variable here before development !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    public void OnInitializationComplete()
    {
        Debug.Log("UnityAds init complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"UnityAds init failed {error} - {message}");
    }
}
