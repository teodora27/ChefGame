using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MuzicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    public static MuzicManager Instanta { get; private set; }
    private AudioSource audio_source;
    public float volum_muzica=.7f;

    private void Awake()
    {
        Instanta = this;
        audio_source = GetComponent<AudioSource>(); 
        volum_muzica = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .3f);
        audio_source.volume = volum_muzica;

    }


    public void SchimbaVolumul()
    {
        volum_muzica += .1f;
        if (volum_muzica > 1f)
        {
            volum_muzica = 0f;
        }
        audio_source.volume = volum_muzica;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volum_muzica);
        PlayerPrefs.Save();
    }
    public float GetVolum()
    {
        return volum_muzica;
    }
}
