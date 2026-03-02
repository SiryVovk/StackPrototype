using UnityEngine;
using GoogleMobileAds.Api;
public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    private RewardedAd rewardedAdInstance;
    private InterstitialAd interstitialAd;

    private const string rewardedId = "ca-app-pub-3940256099942544/5224354917";
    private const string interstitialId = "ca-app-pub-3940256099942544/1033173712";

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MobileAds.Initialize(_ => {
            LoadReward();
        });
    }

    public void LoadReward()
    {
        if (rewardedAdInstance != null)
        {
            rewardedAdInstance.Destroy();
            rewardedAdInstance = null;
        }

        AdRequest adRequest = new AdRequest();

        RewardedAd.Load(rewardedId, adRequest,(RewardedAd rewardedAd, LoadAdError error) =>
        {
            if (error != null || rewardedAd == null)
            {
                Debug.LogError("rewarded  ad failed to load an ad " +
                                "with error : " + error);
                return;
            }
            Debug.Log("Rewarded interstitial ad loaded with response : "
          + rewardedAd.GetResponseInfo());

            rewardedAdInstance = rewardedAd;
        }
        );
    }
}
