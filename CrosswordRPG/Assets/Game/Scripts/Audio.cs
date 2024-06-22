using System.Collections.Generic;
using UnityEngine;

public class Audio 
{
    public bool MuteSounds => _sounds.mute;
    public bool MuteMusic => _music.mute;

    private AudioSource _sounds;
    private AudioSource _music;
    private List<AudioClip> _joystickSounds;
    private AudioClip _uiSound;
    private AudioClip _winSound;
    private int _joystickIndex;

    public Audio()
    {
        _sounds = CreateAudioSource(Game.Instance.transform, 0.4f);
        _music = CreateAudioSource(_sounds.transform, 0.1f);

        _music.loop = true;
        _music.clip = Resources.Load<AudioClip>("Audio/Music/Music");
        _music.Play();
        _music.mute = true;

        _joystickSounds = new List<AudioClip>();
        _joystickSounds.AddRange(Resources.LoadAll<AudioClip>("Audio/Joystick"));
        _joystickIndex = 0;
        _uiSound = Resources.Load<AudioClip>("Audio/UI/UISound");
        _winSound = Resources.Load<AudioClip>("Audio/UI/WinSound");

        Game.Instance.Advertisement.OnAdvShowEvent += OnAdvShow;
        Game.Instance.Advertisement.OnAdvCloseEvent += OnAdvClose; 
    }

    public void InvokeJoystick()
    {
        _sounds.PlayOneShot(_joystickSounds[_joystickIndex]);
        _joystickIndex++;
        if (_joystickIndex >= _joystickSounds.Count ) _joystickIndex = 0;
    }

    public void InvokeUI()
    {
        _sounds.PlayOneShot(_uiSound);
    }

    public void InvokeWin()
    {
        _sounds.PlayOneShot(_winSound);
    }

    public void ChangeMuteSounds()
    {
        _sounds.mute = !_sounds.mute;
    }

    public void ChangeMuteMusic()
    {
        _music.mute = !_music.mute;
    }

    private AudioSource CreateAudioSource(Transform parent, float volume)
    {
        var source = new GameObject("Audio").AddComponent<AudioSource>();
        source.gameObject.transform.parent = parent;
        source.volume = volume;
        return source;
    }

    private void OnAdvShow()
    {
        _sounds.gameObject.SetActive(false);
    }

    private void OnAdvClose()
    {
        _sounds.gameObject.SetActive(true);
    }
}