using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public TMP_Text TimerText;
    public bool timerActive;
    private float currentTime;

    private void OnEnable()
    {
        PlayerEvents.E_PlayerSpawned += HandleSpawnEvent;
    }

    private void OnDisable()
    {
        PlayerEvents.E_PlayerSpawned -= HandleSpawnEvent;
    }

    void Start()
    {
        currentTime = 0f;
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime * 1000; // Accumulate milliseconds
            TimeSpan time = TimeSpan.FromMilliseconds(currentTime); // Convert milliseconds to TimeSpan

            // Format the timer as Min:Sec:Mil
            string formattedTime = $"{time.Minutes:D2}:{time.Seconds:D2}:{time.Milliseconds:D3}";
            TimerText.text = formattedTime;
        }
    }

    void HandleSpawnEvent()
    {
        timerActive = true;
    }
}
