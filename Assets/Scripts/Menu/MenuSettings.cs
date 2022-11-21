using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Toggle fullScreenToggle;
    public Toggle localServerToggle;

    //public Dropdown resolutionDropdown;

    //Resolution[] resolutions;

    void UpdateFullScreenCheckbox() {
        fullScreenToggle.isOn = Screen.fullScreen;
    }

    public void UpdateLocalServerCheckbox() {
        if(Server.mainServer == Server.webServer) {
            Server.mainServer = "http://localhost/school-management-system/";
            localServerToggle.isOn = true;
        }
        else {
            Server.mainServer = "https://anemostudy.x10.mx";
            localServerToggle.isOn = false;
        }
    }

    void OnEnable() {
        UpdateFullScreenCheckbox();
        if(fullScreenToggle.isOn) {
            Screen.fullScreen = true;
        }

        if(Server.mainServer == Server.webServer) {
            localServerToggle.isOn = false;
        }
        else {
            localServerToggle.isOn = true;
        }        
    }

    void Start ()
    {
        UpdateFullScreenCheckbox();
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