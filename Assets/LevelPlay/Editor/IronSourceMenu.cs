#if LEVELPLAY_DEPENDENCIES_INSTALLED
using System.IO;
using UnityEditor;
using UnityEngine;
using Unity.Services.LevelPlay;
using Unity.Services.LevelPlay.Editor.Analytics;

#pragma warning disable 0618

public class IronSourceMenu : Editor
{
    const string IntegrationManagerMenuTitle = "Network Manager";

    [MenuItem("Ads Mediation/Documentation", false, 0)]
    public static void Documentation()
    {
        LevelPlayEditorAnalytics.Instance.SendEventEditorClick(
            LevelPlayEditorAnalytics.LevelPlayComponent.TopMenuAdsMediation,
            LevelPlayEditorAnalytics.LevelPlayAction.OpenDocumentation);

        Application.OpenURL("https://developers.is.com/ironsource-mobile/unity/unity-plugin/");
    }

    [MenuItem("Ads Mediation/SDK Change Log", false, 1)]
    public static void ChangeLog()
    {
        LevelPlayEditorAnalytics.Instance.SendEventEditorClick(
            LevelPlayEditorAnalytics.LevelPlayComponent.TopMenuAdsMediation,
            LevelPlayEditorAnalytics.LevelPlayAction.OpenChangelog);

        Application.OpenURL("https://developers.is.com/ironsource-mobile/unity/sdk-change-log/");
    }

    [MenuItem("Ads Mediation/" + IntegrationManagerMenuTitle, false, 2)]
    public static void SdkManagerProd()
    {
        LevelPlayEditorAnalytics.Instance.SendEventEditorClick(
            LevelPlayEditorAnalytics.LevelPlayComponent.TopMenuAdsMediation,
            LevelPlayEditorAnalytics.LevelPlayAction.OpenNetworkManager);

        IronSourceDependenciesManager.ShowISDependenciesManager();
    }

    [MenuItem("Ads Mediation/Developer Settings/LevelPlay Mediation Settings", false, 3)]
    public static void mediationSettings()
    {
        LevelPlayEditorAnalytics.Instance.SendEventEditorClick(
            LevelPlayEditorAnalytics.LevelPlayComponent.TopMenuDeveloperSettings,
            LevelPlayEditorAnalytics.LevelPlayAction.OpenLevelPlayMediationSettings);

        var path = "Assets/LevelPlay/Resources";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }


        var ironSourceMediationSettings = Resources.Load<IronSourceMediationSettings>(IronSourceConstants.IRONSOURCE_MEDIATION_SETTING_NAME);
        if (ironSourceMediationSettings == null)
        {
            LevelPlayLogger.LogWarning(IronSourceConstants.IRONSOURCE_MEDIATION_SETTING_NAME + " can't be found, creating a new one...");
            ironSourceMediationSettings = CreateInstance<IronSourceMediationSettings>();
            AssetDatabase.CreateAsset(ironSourceMediationSettings, IronSourceMediationSettings.IRONSOURCE_SETTINGS_ASSET_PATH);
            ironSourceMediationSettings = Resources.Load<IronSourceMediationSettings>(IronSourceConstants.IRONSOURCE_MEDIATION_SETTING_NAME);
        }

        Selection.activeObject = ironSourceMediationSettings;
    }

    [MenuItem("Ads Mediation/Developer Settings/Mediated Network Settings", false, 4)]
    public static void mediatedNetworkSettings()
    {
        LevelPlayEditorAnalytics.Instance.SendEventEditorClick(
            LevelPlayEditorAnalytics.LevelPlayComponent.TopMenuDeveloperSettings,
            LevelPlayEditorAnalytics.LevelPlayAction.OpenMediatedNetworkSettings);

        var path = IronSourceConstants.IRONSOURCE_RESOURCES_PATH;

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var ironSourceMediatedNetworkSettings = Resources.Load<IronSourceMediatedNetworkSettings>(IronSourceConstants.IRONSOURCE_MEDIATED_NETWORK_SETTING_NAME);
        if (ironSourceMediatedNetworkSettings == null)
        {
            LevelPlayLogger.LogWarning(IronSourceConstants.IRONSOURCE_MEDIATED_NETWORK_SETTING_NAME + " can't be found, creating a new one...");
            ironSourceMediatedNetworkSettings = CreateInstance<IronSourceMediatedNetworkSettings>();
            AssetDatabase.CreateAsset(ironSourceMediatedNetworkSettings, IronSourceMediatedNetworkSettings.MEDIATION_SETTINGS_ASSET_PATH);
            ironSourceMediatedNetworkSettings = Resources.Load<IronSourceMediatedNetworkSettings>(IronSourceConstants.IRONSOURCE_MEDIATED_NETWORK_SETTING_NAME);
        }

        Selection.activeObject = ironSourceMediatedNetworkSettings;
    }

    [MenuItem("Ads Mediation/Troubleshooting", false, 5)]
    public static void TroubleShootingGuide()
    {
        LevelPlayEditorAnalytics.Instance.SendEventEditorClick(
            LevelPlayEditorAnalytics.LevelPlayComponent.TopMenuAdsMediation,
            LevelPlayEditorAnalytics.LevelPlayAction.OpenTroubleShootingGuide);

        Application.OpenURL("https://docs.unity.com/monetization-dashboard/en-us/manual/editor-levelplay-integration#Troubleshooting");
    }
}
#endif
