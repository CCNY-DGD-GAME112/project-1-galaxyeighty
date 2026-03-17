
using UnityEngine;



public class AudioMngr : MonoBehaviour
{
    public static AudioMngr Instance;
    private AudioSource mainTrack, bossTrack;
    public AudioClip bgm;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            mainTrack = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
       if(bgm != null)
        {
            PlayBGM(false, bgm);
        }

    }

    public void PlayBGM(bool resetSong, AudioClip audioclip = null)
    {
        if (audioclip != null)
        {
            mainTrack.clip = audioclip;
        }
        else if (mainTrack.clip != null)
        {
            if (resetSong)
            {
                mainTrack.Stop();
            }
            mainTrack.Play();
        }
    }

    public void PauseBGM()
    {
        mainTrack.Pause();
    }
}
