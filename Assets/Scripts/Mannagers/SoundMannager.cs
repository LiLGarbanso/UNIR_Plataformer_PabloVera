using System.Collections;
using UnityEngine;

public class SoundMannager : MonoBehaviour
{
    public static SoundMannager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource, ambienceSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume = 0.8f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float ambienceVolume = 1f;
    public float pitchRandomness = 0.1f;

    [Header("Audio Clips")]
    public AudioClip musicaAmbiente;
    public AudioClip musicaVictoria, musicaGameOver;

    private Coroutine cSound;
    public float musicDelay;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
        ambienceSource.volume = ambienceVolume;
    }

    public void Inicio()
    {
        if (musicaAmbiente != null) PlayAmbience(musicaAmbiente, true);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.pitch = 1f + Random.Range(-pitchRandomness, pitchRandomness);
        sfxSource.PlayOneShot(clip, volume * sfxVolume);
    }
    public void PlaySFX_Pitch(AudioClip clip, float volume = 1f, float ptch = 1f)
    {
        ambienceSource.PlayOneShot(clip, volume * sfxVolume);
    }

    public void PlayAmbience(AudioClip clip, bool loop = true)
    {
        if (ambienceSource.clip == clip) return;

        ambienceSource.clip = clip;
        ambienceSource.loop = loop;
        ambienceSource.volume = ambienceVolume;
        ambienceSource.Play();
    }


    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = volume;
    }

    public void PararSonido()
    {
        StopAllCoroutines();
        ambienceSource.Stop();
        musicSource.Stop();
        sfxSource.Stop();
    }

    public void ReproducirSiguienteCancion(AudioClip clip)
    {
        if (cSound != null)
            StopCoroutine(cSound);

        cSound = StartCoroutine(WaitUntilMusicEnds(clip));
    }

    IEnumerator WaitUntilMusicEnds(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();

        yield return new WaitWhile(() => musicSource.isPlaying);
        yield return new WaitForSeconds(musicDelay);

        EventBus.SiguienteCancion();
    }
}
