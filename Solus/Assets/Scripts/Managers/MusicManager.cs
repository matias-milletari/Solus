using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public float soundTrackVolume = 1;
    public float initialVolume = 1;
    public float volumeRampSpeed = 4;
    public bool playOnStart = true;
    public List<AudioSource> audioSources;

    private AudioSource activeAudio, fadeAudio;
    private float volumeVelocity, fadeVelocity;
    private float volume;
    private Stack<string> trackStack = new Stack<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PushTrack(string name)
    {
        trackStack.Push(name);
        Enqueue(name);
    }

    public void PopTrack()
    {
        trackStack.Pop();

        if (trackStack.Count >= 1)
        {
            Enqueue(trackStack.Peek());
        }
    }

    public void Enqueue(string name)
    {
        audioSources.RemoveAll(x => x == null);

        if (audioSources.All(x => x.name != name))
        {
            var newAudioSource = GameObject.Find(name).GetComponent<AudioSource>();
            audioSources.Add(newAudioSource);
        }

        foreach (var i in audioSources)
        {
            if (i.name != name) continue;

            fadeAudio = activeAudio;
            activeAudio = i;
            if (!activeAudio.isPlaying) activeAudio.Play();
            break;
        }
    }

    public void Play()
    {
        if (activeAudio != null)
            activeAudio.Play();
    }

    public void Stop()
    {
        foreach (var i in audioSources) i.Stop();
    }

    void OnEnable()
    {
        trackStack.Clear();

        if (audioSources.Count > 0)
        {
            activeAudio = audioSources[0];

            foreach (var i in audioSources) i.volume = 0;
            trackStack.Push(audioSources[0].name);
            if (playOnStart) Play();
        }

        volume = initialVolume;
    }

    void Reset()
    {
        audioSources = GetComponentsInChildren<AudioSource>().ToList();
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
    }

    void Update()
    {
        if (activeAudio != null)
            activeAudio.volume = Mathf.SmoothDamp(activeAudio.volume, volume * soundTrackVolume, ref volumeVelocity, volumeRampSpeed, 1);

        if (fadeAudio != null)
        {
            fadeAudio.volume = Mathf.SmoothDamp(fadeAudio.volume, 0, ref fadeVelocity, volumeRampSpeed, 1);

            if (Mathf.Approximately(fadeAudio.volume, 0))
            {
                fadeAudio.Stop();
                fadeAudio = null;
            }
        }
    }
}
