using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class SettingsTable : MonoBehaviour
{
    Resolution[] resolutions;
    TMP_Dropdown resolutionDropdown;

    private void Awake()
    {
        resolutionDropdown = GameObject.Find("ResolutionDropdown").GetComponent<TMP_Dropdown>();
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        int currentResolution = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
        if (File.Exists(Application.persistentDataPath + "/savesettingsdata.json"))
        {
            SetFullScreen(PlayerData.Instance.settingsData.isFullScreen);
            SetResolution(PlayerData.Instance.settingsData.resolutionIndex);
            SetQuality(PlayerData.Instance.settingsData.qualityIndex);
        }
        else
        {
            PlayerData.Instance.settingsData.isFullScreen = Screen.fullScreen;
            PlayerData.Instance.settingsData.resolutionIndex = currentResolution;
            PlayerData.Instance.settingsData.qualityIndex = QualitySettings.GetQualityLevel();
        }
        RefreshTableValue();
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerData.Instance.settingsData.qualityIndex = qualityIndex;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerData.Instance.settingsData.isFullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerData.Instance.settingsData.resolutionIndex = resolutionIndex;
    }

    public void RefreshTableValue()
    {
        Toggle fullScreenToggle = GameObject.Find("FullScreenToggle").GetComponent<Toggle>();
        TMP_Dropdown graphicsDropdown = GameObject.Find("GraphicDropdown").GetComponent<TMP_Dropdown>();

        fullScreenToggle.isOn = PlayerData.Instance.settingsData.isFullScreen;
        graphicsDropdown.value = PlayerData.Instance.settingsData.qualityIndex;
        resolutionDropdown.value = PlayerData.Instance.settingsData.resolutionIndex;
    }
}
