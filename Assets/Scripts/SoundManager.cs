using UnityEngine;

public enum SoundType
{
    STARTMUSIC,
    BGM,
    JUMP,
    SLIDE,
    HIT,
    COLLECT
}
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

     public static void PlaySoundLoop(SoundType sound)
    {
        instance.audioSource.clip = instance.soundList[(int)sound];
        instance.audioSource.loop = true;
        instance.audioSource.volume = 1f;
        instance.audioSource.Play();
    }

    public static void StopMusic()
    {
        instance.audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
