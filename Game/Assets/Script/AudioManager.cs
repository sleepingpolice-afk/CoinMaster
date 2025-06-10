using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SoundEffect
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;

    public List<SoundEffect> soundEffects;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            PlayBackgroundMusic();
        }
        else
        {
            StopBackgroundMusic();
        }
    }

    public void PlaySFX(string sfxName)
    {
        if (sfxSource == null)
        {
            Debug.LogWarning("SFX Source not assigned in AudioManager.", this);
            return;
        }

        SoundEffect sfx = soundEffects.Find(sound => sound.name == sfxName);

        if (sfx != null && sfx.clip != null)
        {
            sfxSource.PlayOneShot(sfx.clip);
        }
        else
        {
            Debug.LogWarning("Sound effect named '" + sfxName + "' not found or clip not assigned in AudioManager.", this);
        }
    }

    public void PlayBackgroundMusic()
    {
        if(SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (backgroundMusicSource != null && backgroundMusicSource.clip != null)
            {
                if (!backgroundMusicSource.isPlaying)
                {
                    backgroundMusicSource.Play();
                }
            }
            else
            {
                Debug.LogWarning("Background music source or clip not assigned in AudioManager.", this);
            }
        }
        else
        {
            StopBackgroundMusic();
        }
    }

    public void StopBackgroundMusic()
    {
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
    }
}
