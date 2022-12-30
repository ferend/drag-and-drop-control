using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public const string walletBalancePrefsKey = "walletTotalBalance";
    public const float walletBalanceStartingDefault = 0f;
    
    #region UI
    public const float defaultTransitionDuration = 0.25f;
    public const float overlayTransitionDuration = 0.5f;
    public const float splashScreenDuration = 2.0f;
    public const float popupOpenDuration = 0.5f;
    public const float popupUnderlayTransitionDuration = 0.5f;
    public const float popupCloseDuration = 0.5f;
    #endregion
}
