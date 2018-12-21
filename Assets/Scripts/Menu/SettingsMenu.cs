using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Slider volumeSlider;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);

        if (PlayerPrefs.HasKey("ResolutionIndex"))
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex");
        else
            resolutionDropdown.value = currentResolutionIndex;

        resolutionDropdown.RefreshShownValue();

        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            qualityDropdown.value = PlayerPrefs.GetInt("QualityLevel");
            SetQuality(PlayerPrefs.GetInt("QualityLevel"));
        }

        else
            qualityDropdown.value = QualitySettings.GetQualityLevel();

        //Load saved volume
        SetVolume(PlayerPrefs.GetFloat("GlobalVolume"));
        volumeSlider.value = PlayerPrefs.GetFloat("GlobalVolume");

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("GlobalVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = !isFullScreen;
    }
}
