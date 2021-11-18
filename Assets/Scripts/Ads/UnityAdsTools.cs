using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public sealed class UnityAdsTools : MonoBehaviour, IAdsShower, IUnityAdsListener
{
    private const string GameId = "4459009";
    private const string InterstitialPlace = "Interstitial_Android";


    private Action _callbackSuccessShowVideo;
      
    private void Start()
    {
        Advertisement.Initialize (GameId, true);
    }
      
    public void ShowInterstitial()
    {
        _callbackSuccessShowVideo = null;
        Advertisement.Show(InterstitialPlace);
    }

    public void OnUnityAdsReady(string placementId)
    {
          
    }

    public void OnUnityAdsDidError(string message)
    {
          
    }

    public void OnUnityAdsDidStart(string placementId)
    {
          
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
            _callbackSuccessShowVideo?.Invoke();
    }
}