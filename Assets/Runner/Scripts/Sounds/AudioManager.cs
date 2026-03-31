using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}

    [Header("References")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _musicSource;
    [Header("Music Clips")]
    [SerializeField] private AudioClip _mainThemeMusic;

    private readonly string[] VolumeParams = { "MasterVolume", "MusicVolume", "SFXVolume" };

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadAllVolume();
    }

    private void LoadAllVolume()
    {
        foreach (var param in VolumeParams)
        {
            float volume = PlayerPrefs.GetFloat(param, 1f);
            SetVolume(param, volume);
        }
    }

    public void SetVolume(string exposedParam, float value01)
    {
        float volume = Mathf.Log10(Mathf.Clamp(value01, 0.0001f, 1f)) * 20;
        _audioMixer.SetFloat(exposedParam, volume);
        PlayerPrefs.SetFloat(exposedParam, value01);
    }

    public float GetVolume(string exposedParam)
    {
        return PlayerPrefs.GetFloat(exposedParam, 1f);
    }
}
