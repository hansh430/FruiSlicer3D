using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region Singleton
    public static Timer Instance;
    #endregion
    #region Dependencies
    [SerializeField] private TMP_Text timerText; 
    [SerializeField] private float countdownTime = 60f; 
    private float timer;
    #endregion
    #region MonoBehaviour Methods
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        timer = countdownTime;
        UpdateTimerUI();
        InvokeRepeating("UpdateCountdown", 1f, 1f);
    }
    #endregion

    #region Functionality Methods
    void UpdateCountdown()
    {
        timer -= 1f; 
        UpdateTimerUI();

        if (timer <= 0f)
        {
            ResetTimer();
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = FormatTime(timer);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void ResetTimer()
    {
        timer = 0f;
        Debug.Log("Time's up!");
        CancelInvoke("UpdateCountdown");
    }
    #endregion
}
