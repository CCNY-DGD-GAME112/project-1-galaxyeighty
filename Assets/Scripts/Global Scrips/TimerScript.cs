using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float time;
    public TextMeshProUGUI timerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timerText.text = Mathf.Floor(time).ToString();
    }
}
