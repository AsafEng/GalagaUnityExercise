using System;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class AudioController : Singleton<AudioController>
{
    //Sound data
    public SoundModel[] Sounds;

    //Current playing bg
    private string _currentMusicName = "";

    void Start()
    {
        //Init all sound data models
        foreach (SoundModel s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }

    public void Play(string name)
    {
        //Find the require sound by name
        SoundModel searchSound = Array.Find(Sounds, sound => sound.Name == name);

        if (searchSound == null)
        {
            return;
        }

        //Effect or Music
        if (searchSound.IsEffect)
        {
            //Randomize sound effect every time it is played
            RandomizeEffect(searchSound);
        }
        else
        {
            //Change background music
            _currentMusicName = name;
            if (GetIsPlaying())
            {
                return;
            }
        }

        searchSound.Source.Play();
    }

    public void StopPlaying(string sound)
    {
        SoundModel s = Array.Find(Sounds, item => item.Name == sound);
        if (s == null)
        {
            return;
        }

        if (s.IsEffect)
        {
            s.Source.volume = s.Volume * (1f + Random.Range(-0.2f, 0.2f));
            s.Source.pitch = s.Pitch * (1f + Random.Range(-0.2f, 0.2f));
        }

        s.Source.Stop();
    }

    public void RandomizeEffect(SoundModel s)
    {
        //Randomize sound
        s.Source.pitch = s.Pitch + Random.Range(-0.1f, 0.1f);
    }

    public bool GetIsPlaying()
    {
        SoundModel s = Array.Find(Sounds, sound => sound.Name == _currentMusicName);
        if (s == null)
        {
            return true;
        }

        return s.Source.isPlaying;
    }

    public void ChangeVolume(bool soundOn)
    {
        AudioListener.volume = soundOn ? 1f : 0f;
    }
}
