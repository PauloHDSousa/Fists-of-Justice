using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    PlayerPrefsManager prefsManager = new PlayerPrefsManager();

    [Header("Song")]
    [SerializeField]
    AudioMixerGroup soundMixer;

    [Header("SFX")]
    [SerializeField]
    AudioMixerGroup soundEffectMixer;

    void Start()
    {
        float songVolume = 1;
        float sfxVolume = 1;

        if (!prefsManager.HasKey(PlayerPrefsManager.PrefKeys.Volume))
            prefsManager.SaveFloat(PlayerPrefsManager.PrefKeys.Volume, songVolume);

        if (!prefsManager.HasKey(PlayerPrefsManager.PrefKeys.SFX))
            prefsManager.SaveFloat(PlayerPrefsManager.PrefKeys.SFX, sfxVolume);

        songVolume = prefsManager.GetFloat(PlayerPrefsManager.PrefKeys.Volume);
        sfxVolume = prefsManager.GetFloat(PlayerPrefsManager.PrefKeys.SFX);

        soundEffectMixer.audioMixer.SetFloat("SFX Volume", Mathf.Log10(sfxVolume) * 20);
        soundMixer.audioMixer.SetFloat("Song Volume", Mathf.Log10(songVolume) * 20);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadGame()
    {
        LoadScene("Map");
    }

    public void LoadOptions()
    {
        LoadScene("Options");
    }

    public void LoadPratice()
    {
        LoadScene("Pratice");
    }


    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
