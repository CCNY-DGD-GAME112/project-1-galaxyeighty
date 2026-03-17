using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScenes : MonoBehaviour
{
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 4 (boss)")
            AudioMngr.Instance.GetComponent<AudioSource>().Pause();
        


    }
}
