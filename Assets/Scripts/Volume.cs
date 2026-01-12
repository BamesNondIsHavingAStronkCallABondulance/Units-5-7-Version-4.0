using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public const string MixerMusic = "MusicVolume";
    public const string MixerSfx = "SFXVolume";

    public bool mute = false;
    float stored = 0;

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 0.5f);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }
    void SetMusicVolume(float value)
    {
        if (value != 0.0001f)
        {
            stored = value;
        }
        mixer.SetFloat(MixerMusic, Mathf.Log10(value) * 20);
    }


    public void Muted()
    {
        if (mute == false)
        {
            mute = true;
            SetMusicVolume(0.0001f);
        }
        else if(mute == true)
        {
            mute = false;
            SetMusicVolume(stored);
        }
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MixerSfx, Mathf.Log10(value) * 20);
    }
}
