using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance;
    #endregion

    [Header("<color=orange>Audio</color>")]
    [SerializeField] private AudioMixer _mixer;
    public AudioSource GrabAudioSource;
    [SerializeField] private AudioClip[] _sfxClips;

    private AudioSource _source;

    private float _masterVol = 0.0f;
    public float MasterVol { get { return _masterVol; } set { _masterVol = value; } }
    private float _musicVol = 0.0f;
    public float MusicVol { get { return _musicVol; } set { _musicVol = value; } }
    private float _sfxVol = 0.0f;
    public float SfxVol { get { return _sfxVol; } set { _sfxVol = value; } }

    private void Awake()
    {
        #region Instance
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        _source = GetComponent<AudioSource>();
    }

    public void SetMasterVolume(float value)
    {
        _masterVol = value;
        _mixer.SetFloat("Master", Mathf.Log10(value) * 20.0f);
    }

    public void SetMusicVolume(float value)
    {
        _musicVol = value;
        _mixer.SetFloat("Music", Mathf.Log10(value) * 20.0f);
    }

    public void SetSfxVolume(float value)
    {
        _sfxVol = value;
        _mixer.SetFloat("SFX", Mathf.Log10(value) * 20.0f);
    }

    public void PlayClip(AudioClip clip)
    {
        if (_source.clip == clip) return;

        _source.Stop();

        _source.clip = clip;

        _source.Play();
    }

    public void PlaySFXClip(int clip)
    {
        switch (clip)
        {
            case 0:
                GrabAudioSource.clip = _sfxClips[0];
                GrabAudioSource.Play();
                Debug.Log("Reproduciendo SFX clip 0: " + _sfxClips[0].name);
                break;
            case 1:
                GrabAudioSource.clip = _sfxClips[1];
                GrabAudioSource.Play();
                Debug.Log("Reproduciendo SFX clip 1: " + _sfxClips[1].name);
                break;
            default:
                Debug.LogWarning("índice de clip inválido: " + clip);
                break;
        }
    }
}
