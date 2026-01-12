using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{

    public void L1Load()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuLoad()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayMusicClip(string name)
    {
        Sound s = Array.Find(AudioManager.instance.sounds, sound => sound.name == name);
        s.musicSource.Play();
    }

    public void PlaySFXClip(string name)
    {
        Sound s = Array.Find(AudioManager.instance.sounds, sound => sound.name == name);
        s.sfxSource.Play();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
