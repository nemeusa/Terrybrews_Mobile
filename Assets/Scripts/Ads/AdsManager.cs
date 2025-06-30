using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RewardedAd))]
public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

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

    public bool canUseAds = true;
    [SerializeField] RewardedAd _rewardedAd;

    private void Start()
    {
        if(_rewardedAd == null)
        {
            _rewardedAd = GetComponent<RewardedAd>();
            _rewardedAd.LoadRewardedAd();
        }
    }

    public void ExecuteRewardedAd()
    {
        _rewardedAd.ShowRewardedAd();
    }
}
