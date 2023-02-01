using UnityEngine;

[System.Serializable]
public class SoundModel
{
    public AudioClip Clip;
    public string Name;
    public bool IsEffect;
    public bool Loop;

    [Range(0f, 1f)]
    public float Volume = 0.5f;
    [Range(0f, 1f)]
    public float Pitch = 1f;

    [HideInInspector]
    public AudioSource Source;
}
