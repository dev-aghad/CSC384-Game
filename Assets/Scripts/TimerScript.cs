using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 60f;
    [SerializeField] private TMP_Text timerText;

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();

        if (timeRemaining <= 0)
        {
            timerText.text = "Game Over";
        }
    }
}
