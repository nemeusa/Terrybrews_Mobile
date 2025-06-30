using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinderRewardButton : MonoBehaviour
{
    public void ExecuteRewardedAd()
    {
        AdsManager.Instance.ExecuteRewardedAd();
    }
}
