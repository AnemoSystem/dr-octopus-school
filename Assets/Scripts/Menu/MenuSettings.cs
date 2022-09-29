using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Toggle fullScreenToggle;

    //public Dropdown resolutionDropdown;

    //Resolution[] resolutions;
    void Start ()
    {
        fullScreenToggle.isOn = false;
        /*
        if(resolutionDropdown != null)
            setupResolutions();
        */
    }
/*
    void setupResolutions() {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
        }
        resolutionDropdown.AddOptions(options);
    }
*/    
    public void SetVolume(float volume){
        audioMixer.SetFloat("volume", volume);
    }
    
    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}