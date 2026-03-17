using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Level : MonoBehaviour
{
    public static Level instance;

    uint numEnemies = 0;

    string[] levels = { "Level 1", "Level 2", "Level 3", "Level 4 (boss)" };
    int currentLevel = 1;

    int score = 0;
    TMP_Text scoreTracker;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            scoreTracker = GameObject.Find("ScoreTracker").GetComponent<TMP_Text>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
 

    public void ResetLevel()
    {
        foreach (GunScript b in GameObject.FindObjectsByType<GunScript>(FindObjectsSortMode.None)) 
        {
            Destroy(b.gameObject);
        }
        numEnemies = 0;
        score = 0;
        AddScore(score);
        string sceneName = levels[currentLevel - 1];
        SceneManager.LoadScene(sceneName);
    }

    public void AddScore(int addAmount)
    {
        score += addAmount;
        scoreTracker.text = score.ToString();
    }

    public void AddEnemy()
    {
        numEnemies++;
    }

    public void RemoveEnemy()
    {
        numEnemies--;
    }
}
