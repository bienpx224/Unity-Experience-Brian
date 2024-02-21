using System.Collections.Generic;
using GoogleMobileAds.Ump.Api;
using UnityEngine;
using UnityEngine.UI;

public class UMPManager : Singleton<UMPManager>
{
    [SerializeField] private bool enableUMPTest = false;

    [SerializeField, Tooltip("Button to show the privacy options form.")]
    private Button _privacyButton;

    [SerializeField] private Button _resetButton;

    [SerializeField] public bool privacyStatusRequired;
    private ConsentForm _consentForm;

    private void Start()
    {
        // Disable the privacy settings button.
        UpdatePrivacyButton();

        RequestConsentInfo();

        if (_privacyButton != null)
        {
            _privacyButton.onClick.AddListener(ShowPrivacyOptionsForm);
        }

        if (_resetButton != null)
        {
            _resetButton.onClick.AddListener(ResetConsentInformation);
        }
    }


    ///Summary
    ///Request Consent Information
    ///Warning: Ads can be preloaded by the Google Mobile Ads SDK or mediation partner SDKs
    ///upon calling MobileAds.Initialize(). If you need to obtain consent from users in the European Economic Area (EEA), set any request-specific flags, such as tagForChildDirectedTreatment or tag_for_under_age_of_consent, or otherwise take action before loading ads.
    ///Ensure you do this before initializing the Google Mobile Ads SDK.
    ///Summary
    void RequestConsentInfo()
    {
        ResetConsentInformation();
        var debugSettings = new ConsentDebugSettings();
        if (enableUMPTest)
        {
            debugSettings = new ConsentDebugSettings
            {
                // Geography appears as in EEA for debug devices.
                DebugGeography = DebugGeography.EEA,
                TestDeviceHashedIds = new List<string>
                {
                    "965E4A26737DF85475A353251709C315"
                }
            };
        }
        else
        {
            debugSettings = new ConsentDebugSettings
            {
                TestDeviceHashedIds = new List<string>
                {
                }
            };
        }

        // Set tag for under age of consent.
        // Here false means users are not under age of consent.
        ConsentRequestParameters request = new ConsentRequestParameters
        {
            TagForUnderAgeOfConsent = false,
            ConsentDebugSettings = debugSettings,
        };

        // Check the current consent information status.
        ConsentInformation.Update(request, OnConsentInfoUpdated);
    }

    void OnConsentInfoUpdated(FormError consentError)
    {
        if (consentError != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(consentError);
            return;
        }

        // If the error is null, the consent information state was updated.
        // You are now ready to check if a form is available.
        ConsentForm.LoadAndShowConsentFormIfRequired((FormError formError) =>
        {
            // Enable the change privacy settings button.
            UpdatePrivacyButton();

            if (formError != null)
            {
                // Consent gathering failed.
                UnityEngine.Debug.LogError(consentError);
                return;
            }

            // Consent has been gathered.
            if (ConsentInformation.CanRequestAds())
            {
                AdsManager.Instance.LoadAd();
            }
        });
    }

    public void UpdatePrivacyButton()
    {
        if (_privacyButton != null)
        {
            _privacyButton.interactable =
                ConsentInformation.PrivacyOptionsRequirementStatus ==
                PrivacyOptionsRequirementStatus.Required;
        }

        privacyStatusRequired = ConsentInformation.PrivacyOptionsRequirementStatus ==
                                PrivacyOptionsRequirementStatus.Required;
    }

    /// <summary>
    /// Shows the privacy options form to the user.
    /// </summary>
    public void ShowPrivacyOptionsForm()
    {
        Debug.Log("Showing privacy options form.");

        ConsentForm.ShowPrivacyOptionsForm((FormError showError) =>
        {
            UpdatePrivacyButton();

            if (showError != null)
            {
                Debug.LogError("Error showing privacy options form with error: " + showError.Message);
            }
        });
    }

    /// <summary>
    /// Reset ConsentInformation for the user.
    /// </summary>
    public void ResetConsentInformation()
    {
        ConsentInformation.Reset();
        UpdatePrivacyButton();
    }
}