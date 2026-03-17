using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSwapper : MonoBehaviour
{
    public static MusicSwapper Instance;
    public AudioSource mainTrack;
    public AudioSource bossTrack;
    public int TrackSwap;
    public int TrackHist;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()

    {
        TrackSwap = 0;

        if (TrackSwap == 0)
        {
            mainTrack.Play();
            TrackHist = 1;
        }
        else if (TrackSwap == 1)
        {
            bossTrack.Play();
            TrackHist = 2;
        }

        if (Instance == null)
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
        // Update is called once per frame
        void Update()
        {
            if (SceneManager.GetActiveScene().name == "Level 4 (boss)")
               TrackSwap++;
        }
    
}
