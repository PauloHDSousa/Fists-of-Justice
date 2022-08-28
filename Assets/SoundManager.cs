using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Song")]
    [SerializeField]
    Slider soundSlider;
    [SerializeField]
    AudioMixerGroup soundMixer;

    [Header("SFX")]
    [SerializeField]
    Slider soundEffectsSlider;
    [SerializeField]
    AudioMixerGroup soundEffectMixer;


    PlayerPrefsManager prefsManager = new PlayerPrefsManager();
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

        soundSlider.value = songVolume;
        soundEffectsSlider.value = sfxVolume;

        soundEffectMixer.audioMixer.SetFloat("SFX Volume", Mathf.Log10(sfxVolume) * 20);
        soundMixer.audioMixer.SetFloat("Song Volume", Mathf.Log10(songVolume) * 20);
    }

    public void ChangeSoundVolume()
    {
        float volumeValue = soundSlider.value;
        prefsManager.SaveFloat(PlayerPrefsManager.PrefKeys.Volume, volumeValue);
        soundMixer.audioMixer.SetFloat("Song Volume", Mathf.Log10(volumeValue) * 20);
    }

    public void ChangeSoundEffectsVolume()
    {
        float volumeValue = soundSlider.value;
        prefsManager.SaveFloat(PlayerPrefsManager.PrefKeys.SFX, volumeValue);
        soundEffectMixer.audioMixer.SetFloat("SFX Volume", Mathf.Log10(volumeValue) * 20);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}