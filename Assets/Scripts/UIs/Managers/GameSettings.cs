using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public bool m_AutoSave;
    public AudioMixer m_Mixer;

    [Header("Default")]
    public float m_DefaultSfx = 75.0f;
    public float m_DefaultMusic = 50.0f;
    public float m_DefaultBrightness = 75.0f;

    [Header("UI")]
    public Slider m_SfxSlider;
    public Slider m_MusicSlider;
    public Slider m_BrightnessSlider;

    [Range(0.0f, 1.0f)]
    public float m_Brightness = 75.0f;
    public Color m_AmbientLight = Color.white;
    public Color m_AmbientDark = Color.black;

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        m_SfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", m_DefaultSfx);
        m_MusicSlider.value = PlayerPrefs.GetFloat("musicVolume", m_DefaultMusic);
        m_Brightness = PlayerPrefs.GetFloat("brightness", m_DefaultBrightness);
        m_BrightnessSlider.value = m_Brightness;
    }

    public void Reset()
    {
        m_SfxSlider.value = m_DefaultSfx;
        SetSfxVolume();

        m_MusicSlider.value = m_DefaultMusic;
        SetMusicVolume();

        m_Brightness = m_DefaultBrightness;
        m_BrightnessSlider.value = m_Brightness;
        SetBrightness();
    }

    public void SetBrightness()
    {
        m_Brightness = m_BrightnessSlider.value;
        RenderSettings.ambientLight = Color.Lerp(m_AmbientDark, m_AmbientLight, m_Brightness); 
    }

    public void SetMusicVolume()
    {
        m_Mixer.SetFloat("musicVolume", m_MusicSlider.value);
    }

    public void SetSfxVolume()
    {
        m_Mixer.SetFloat("sfxVolume", m_SfxSlider.value);
    }

    private void OnDisable()
    {
        if (m_AutoSave)
            Save();
    }

    public void Save()
    {
        float musicVolume = m_DefaultMusic;
        float sfxVolume = m_DefaultSfx;
        m_Mixer.GetFloat("musicVolume", out musicVolume);
        m_Mixer.GetFloat("sfxVolume", out sfxVolume);

        PlayerPrefs.SetFloat("brightness", m_Brightness);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }
}
