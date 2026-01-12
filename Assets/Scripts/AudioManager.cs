using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixer mixer;

    public const string MUSIC_KEY = "MusicVolume";
    public const string SFX_KEY = "SFXVolume";

    bool frontend = true;
    bool dummyScene = false;

    public Sound[] sounds;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;

        }

        foreach (Sound s in sounds)
        {
            s.musicSource = gameObject.AddComponent<AudioSource>();
            s.musicSource.clip = s.clip;
            s.musicSource.outputAudioMixerGroup = s.musicMixer;
            s.musicSource.volume = s.volume;
            s.musicSource.pitch = s.pitch;
            s.musicSource.loop = s.loop;

            s.sfxSource = gameObject.AddComponent<AudioSource>();
            s.sfxSource.clip = s.clip;
            s.sfxSource.outputAudioMixerGroup = s.sfxMixer;
            s.sfxSource.volume = s.volume;
            s.sfxSource.pitch = s.pitch;
            s.sfxSource.loop = s.loop;
        }

        LoadVolume();
    }

    private void Start()
    {
        PlayMusicClip("Frontend");
    }

    private void Update()
    {
        CheckScene();
    }

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 0.5f);

        mixer.SetFloat(Volume.MixerMusic, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(Volume.MixerSfx, Mathf.Log10(sfxVolume) * 20);
    }

    void CheckScene()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            if(frontend)
            {
                StopMusicClip("DummyScene");
                PlayMusicClip("Frontend");
                frontend = false;
                dummyScene = true;
            }
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            if (dummyScene)
            {
                StopMusicClip("Frontend");
                PlayMusicClip("DummyScene");
                dummyScene = false;
                frontend = true;
            }
        }
    }

    public void PlayMusicClip(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.musicSource.Play();
    }

    public void PlaySFXClip(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.sfxSource.Play();
    }

    public void StopMusicClip(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.musicSource.Stop();
    }

    public void StopSFXClip(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.sfxSource.Stop();
    }
}
